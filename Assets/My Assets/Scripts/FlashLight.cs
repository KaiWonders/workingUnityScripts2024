using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light m_Light;
    public float maxBrightness;
    public bool drainOverTime;
    public float minBrightness;
    public float drainRate;

    public KeyCode triggerKey;
    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update(){
        m_Light.intensity = Mathf.Clamp(m_Light.intensity, minBrightness, maxBrightness); //Clamps the intensity to whichever value is set to min max
        
        if(drainOverTime == true && m_Light.enabled == true){ //checks if dot is active
            if (m_Light.intensity > minBrightness){
                m_Light.intensity -= Time.deltaTime * (drainRate/1000); //drainssss :3 sucky sucky
            }
        }
    
        if (Input.GetKeyDown(triggerKey)){ //Toggle the light, change to wanted trigger
            m_Light.enabled = !m_Light.enabled; // toggles light
        } 
    }

    public void ReplaceBattery(float amount){
        m_Light.intensity += amount;
    }

}
/*
if (Input.GetKeyDown(KeyCode.R))
    {
        ReplaceBattery(.3f);
    }
*/

//source: https://www.youtube.com/watch?v=SumkdpuPjLg&ab_channel=JTAGames