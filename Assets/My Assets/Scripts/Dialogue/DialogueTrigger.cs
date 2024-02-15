using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// THIS WORKS NOW!
// Assign this to the element you want to trigger the dialog (ie: an npc, an onject..)
// Write the dialog in the editor
// place this where the trigger/box collision is!!!! it won't work otherwise

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogBox; // Don't forget to actually make this
    public TextMeshProUGUI dialogText;
    public bool PlayerInRange;
    public float textChangeSpeed = 0.5f; // lower = faster.
    public float letterPause = 0.05f; // pause between chars. 0.05 is good to not make it excrutiatingly slow
    // need to implement a show all on click

    //imo this is pretty snappy at 0.5f but 1 or 2 is fine as well if
    //you want to be slower. DO NOT PUT 0 it will read everything in 1 frame
    
    public string[] testDialogLines;
    public string spokenDialoge = "";
    // add new public string[] NAME; for every new dialogue path
    // Don't forget to add the referal to it in Update()

    private DistanceDetection distanceDetection; // gets the stuff from DistanceDetection
    private FreezePlayer freezePlayer; // gets the stuff to freeze


    void Start()
    {
        distanceDetection = FindObjectOfType<DistanceDetection>();  //  looks for DistanceDetection
        if (distanceDetection == null){
            Debug.LogError("DistanceDetection script not found in scene :/ ");
        }

        freezePlayer = FindObjectOfType<FreezePlayer>();  // looks for to Freeze
        if (freezePlayer == null){
            Debug.LogError("Freeze script not found in scene :/ ");
        }
    }

    // Update is called once per frame
    void Update()
        {
            if (distanceDetection != null){
                bool playerInRangeSphere = distanceDetection.playerInRangeSphere;
                 // gets the shit from DistanceDetection

                if (Input.GetKeyDown("space") && playerInRangeSphere){
                    Debug.Log("Pressed space & player in range.");
                    if (!dialogBox.activeInHierarchy){
                           if (freezePlayer != null){ // checks if freeze exists
                            freezePlayer.StopPlayer(); // Freezes
                            }else{Debug.LogWarning("Freeze not set");}
                        dialogBox.SetActive(true);
                        StartCoroutine(runDialogue());
                    }
                }
            
            } else {Debug.LogWarning("DistanceDetection reference not found :()");}
        }   

    void endDialogue(){
        dialogBox.SetActive(false); // Removes dialog box
        freezePlayer.RestartPlayer(); // Unfreezes
    }

    IEnumerator runDialogue() // enumerator cuz otherwise all the text gets shown in one frame
    {

        foreach (string line in testDialogLines){
            foreach(char letter in line){
                spokenDialoge += letter;
                dialogText.text = spokenDialoge;
                yield return new WaitForSeconds (letterPause);
            }
            spokenDialoge = "";
            yield return new WaitForSeconds(textChangeSpeed); // adds a delay between line skipping
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // click

        }
        endDialogue();
    }

}


// COLLIDER METHOD


    // Add a seperate box collider around the triggernpc otherwise this shit won't work
    // make sure it is set as trigger
    // also if the game is 2D:  OnTriggerEnter2D(Collider2D other)
   /* private void OnTriggerEnter(Collider other) {   // checks if player is in range
        if (other.CompareTag("Player")) {
                playerInRangeSphere = true;
                Debug.Log("Player in Range");
             }
        }

    private void OnTriggerExit(Collider other) {    // chekcs if player leaves
        if (other.CompareTag("Player")){
                    PlayerInRange = false;
                    dialogBox.SetActive(false);
                    Debug.Log("Player exit");
        }
     }
    */
