using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionButton : MonoBehaviour{
    //Add this to the thing u want to trigger (ie: in rythm game, the arrows)
    //Don't forget to make colliders on button + triggerer
    // And rigidbody onto collider ( button)

    public bool canBePressed;
    public KeyCode keyToPress; //set keys in editor
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect; // drag the prefabs into the editor
    //dont forget to check if effectlifetime is on!




    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(keyToPress)){ // Removes arrows if key pressed
            if(canBePressed){
                gameObject.SetActive(false);
                //RythmGameManager.rythmInstance.noteHit(); 
                //Add points

                // change the values according to the button placement! if buttons are at 0, make it 0.25 for ex
                if (Mathf.Abs(transform.position.y) > 0.25){
                    RythmGameManager.rythmInstance.normalHit(); Debug.Log ("Normal hit");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);} 
                    

                else if (Mathf.Abs(transform.position.y) > 0.10) {
                    RythmGameManager.rythmInstance.goodHit(); Debug.Log ("Good hit");
                    Instantiate(goodEffect, transform.position, hitEffect.transform.rotation);}

                else {
                    RythmGameManager.rythmInstance.perfectHit(); Debug.Log ("Perfect hit"); 
                    Instantiate(perfectEffect, transform.position, hitEffect.transform.rotation);}
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Activator"){ // Custom tag
            canBePressed = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Activator"){ // Custom tag
            canBePressed = false;
            RythmGameManager.rythmInstance.noteMiss(); 
            Instantiate(missEffect, transform.position, hitEffect.transform.rotation);

        }
    }
}
