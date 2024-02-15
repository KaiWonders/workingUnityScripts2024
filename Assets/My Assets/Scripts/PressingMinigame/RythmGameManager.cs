using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RythmGameManager : MonoBehaviour
{
    //Make empty GameManager object and assign this to it

    // ------------------------------------------------------------

    public AudioSource levelMusic; //Make sure the music is not loop, & play on awake off xx
    public bool startRythmMusic; //Self-explanatory
    public BeatScroller BS;
    // Assign the parent of the notes to this in editor
    public GameObject notesParent;


    private FreezePlayer freezePlayer; // gets the stuff to freeze
     //RN you cant unfreeze


    // ------------ SCORE COUNT -------------------------------------
    public int rythmScore;
    public int noteHitScore = 10;
    public int noteGoodHitScore = 15;
    public int notePerfectHitScore = 20;
    public int noteMissScore = 1;


   // ------------ MULTIPLIERS --------------------------------------
    public int currentMultiplier;
    public int multiplierCount; // tracks the amount of correctly hit notes 
    public int[] multiplierThreshholds; //array (length+1) = max multiplier. the numbers u add in editor are needed correct hits to reach new mutliplier
    // add these in editor


    // --------- TEXT HOLDERS ---------------------------------------

    public TextMeshProUGUI scoreText;
    public GameObject scoreBox;
    public string displayedScore = "0";


    public TextMeshProUGUI multiText;
    public GameObject multiBox;
    public string displayedMulti = "1";


    // ------------------------------------------------------------



    public static RythmGameManager rythmInstance; //makes it possible to refer to this specific gamemanger state :D

    // Start is called before the first frame update
    void Start(){
        rythmInstance = this; // Should make only one 
        currentMultiplier = 1;   


        freezePlayer = FindObjectOfType<FreezePlayer>();  // looks for to Freeze
        if (freezePlayer == null){
            Debug.LogError("Freeze script not found in scene :/ ");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // ----------------- NOTE HIT & SCORE TRACKERS ------------------------------------------- 
        // This starts music, activates score & multiplier display, & freezes player. It also starts the scrolling

        if(!startRythmMusic){ // music off by default :3
            if (Input.GetKeyDown("o") && !startRythmMusic){                 //Change this to whatever you want to start the minigame music

                freezePlayer.StopPlayer();
                // freezePlayer.RestartPlayer(); // Unfreezes

                
                if (!scoreBox.activeInHierarchy && !multiBox.activeInHierarchy&& !notesParent.activeInHierarchy ){
                    scoreBox.SetActive(true); multiBox.SetActive(true); notesParent.SetActive(true);} 
                //Sets score & notes to be visibile
                startRythmMusic = true;
                BS.beatStarted = true; //refers to bool in beatscroller
                //maybe move the freeze effect to here? TBD
                levelMusic.Play();

                
            }

        }
    }
// ----------------- NOTE HIT & SCORE TRACKERS ------------------------------------------- 
// Ads/removes to your total score, and keeps count of multiplier situation


    public void noteHit(){
        Debug.Log("Note hit!");
        
        if (currentMultiplier - 1 < multiplierThreshholds.Length ){ // Ensures that u dont go over max multiplier ( array length + 1)
            multiplierCount++; // adds a level to the tracker
            if (multiplierThreshholds[currentMultiplier-1] <= multiplierCount){
                multiplierCount = 0;
                currentMultiplier++;
            }
        }
        
        scoreText.text = "Score: " + rythmScore.ToString();
        multiText.text = "Multiplier: x" + currentMultiplier.ToString();
    }

    public void normalHit(){
        rythmScore += noteHitScore*currentMultiplier;

        noteHit();

    }
    public void goodHit(){
        rythmScore += noteGoodHitScore*currentMultiplier;
        noteHit();
    }
    public void perfectHit(){
        rythmScore += notePerfectHitScore*currentMultiplier;
        noteHit();
    }

    // ----------------- NOTE MISS  ------------------------------------------- 

    public void noteMiss(){
        Debug.Log("Note missed!");
        multiplierCount = 0;
        currentMultiplier = 1;
        
        rythmScore -= noteMissScore;
        rythmScore = Mathf.Clamp(rythmScore, 0, 99999); // Prevents the score from getting lower than 0 or higher than 99999
        scoreText.text = "Score: " + rythmScore.ToString(); 
        multiText.text = "Multiplier: x" +  currentMultiplier.ToString();


    }

// ----------------------------------------------------------------------------- 
}
