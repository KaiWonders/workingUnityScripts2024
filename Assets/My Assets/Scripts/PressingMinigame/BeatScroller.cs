using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Makes everything fall down :D
//Place this onto the parent of notes


public class BeatScroller : MonoBehaviour
{
    public float beatTempo; //tempo added in editor
    public bool beatStarted; 



    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo /  60f; // how fast moves per second

 
    }

    // Update is called once per frame
    void Update(){
        if (beatStarted){
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            // makes it move if beat is stated
            // only moving on the Y axis so the other two are 0f
        }
    
        

    } 

}
