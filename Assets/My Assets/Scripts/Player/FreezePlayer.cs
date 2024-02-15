using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// PLACE THIS AT SAME LEVEL AS CHARACTER SCRIPT
//otherwise it disappears on build
// not sure why
public class FreezePlayer : MonoBehaviour // Freezes player movement
{
    public Character character;
    void Start(){
        character = GetComponent<Character>();
        if (character == null){
        Debug.LogError("character script not found in scene :/ ");
        }   
    }

    public void StopPlayer(){
        character.Speed = 0f;
    }

    public void RestartPlayer(){
        character.Speed = 5f;
    }

}
