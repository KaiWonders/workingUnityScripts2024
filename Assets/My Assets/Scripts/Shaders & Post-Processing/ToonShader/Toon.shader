Shader "KaiShaders/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		// Colors normally have their RGB values set between 0 and 1;
		// The [HDR] attribute specifies that this color property can have its values set beyond that. 
		// Values can be used for certain kinds of rendering effects, like bloom or tone mapping

		[HDR] // Specular reflection
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1) 	// Reflection colour
		_Glossiness("Glossiness", Float) = 32						//Reflection size
		// Blinn-Phong specular

		[HDR] // Rim lighting
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716

		// Rim location threshold
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1




	}
	SubShader
	{
		Pass{
			Tags{
				"LightMode" = "ForwardBase" // gets light data for our shader
				"PassFlags" = "OnlyDirectional" // makes it only the main directional light 
				//"RenderType" = "Opaque"
				//"RenderPipeline" = "UniversalRenderPipeline"

			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase //Handle when the main directional light does and does not cast shadows.

			
			#include "UnityCG.cginc"
			#include "Lighting.cginc" 								//allows light to influence shader
			#include "AutoLight.cginc"
			
			//DOES NOT WORK IN URP YET IDK WHY!!!!
			
			/*#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"    
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"   */



			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL; // access the GameObject normal 
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1; 							// Calculates world view direction
				SHADOW_COORDS(2) 										//generates a 4-dimensional value with varying precision 
				//														//(depending on the target platform) and assigns it to the TEXCOORD
				//														//semantic at the provided index (in our case, 2).

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v) // Vertex shader
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal); 	// converts the object space to worldspace
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.viewDir = WorldSpaceViewDir(v.vertex); 				// Calculates world view direction

				TRANSFER_SHADOW(o)
				// TRANSFER_SHADOW transforms the input vertex's space to the shadow map's space, and then stores it in the SHADOW_COORD we declared.

				return o;
			}
			
			float4 _Color;
			float4 _AmbientColor; 		// Lets use use ambient colours
			float _Glossiness;			//Specular reflection size
			float4 _SpecularColor;		//Specular reflection colour
			float4 _RimColor;			// Colour of rimlighting
			float _RimAmount;			// Amount of rimlighting
			float _RimThreshold;		// Rim location threshold



			float4 frag (v2f i) : SV_Target // Fragment shader
			{
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				//float lightIntensity = NdotL > 0 ? 1 : 0; 					// modifying the shader into bands

				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow); 	// Smoothstep transition between light n dark
				// the higher the second number, the smoother the blend
				
				float4 light = lightIntensity * _LightColor0; 					//allows light to influence shader
				// _LightColor0 is the color of the main directional light. It is a fixed4 declared in the Lighting.cginc

				// BLINN-PHONG SPECULAR REFLECTION CALCULATOR

				float3 viewDir = normalize(i.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir); // vector between the viewing direction and the light source;
				float NdotH = dot(normal, halfVector);
								//strength of the specular reflection is defined in Blinn-Phong
								//as the dot product between the normal of the surface and the half vector.
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
								// We control the size of the specular reflection using the pow function.
								// We multiply NdotH by lightIntensity to ensure that the reflection is only drawn when the surface is lit
				float specularIntensitySmooth = smoothstep(0.005, 0.08, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;
				// BLINN-PHONG SPECULAR REFLECTION CALCULATOR

				float4 rimDot = 1 - dot(viewDir, normal); // calculate the rim by taking the dot product of the normal and the view direction, and inverting it.
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
  				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);

				float4 rim = rimIntensity * _RimColor;


				float4 sample = tex2D(_MainTex, i.uv);

				return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER" // Cast shadows yippeeeeee <3
	}
}