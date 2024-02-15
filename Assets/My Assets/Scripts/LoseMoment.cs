using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseMoment : MonoBehaviour {

    private void MainMoment() {
        Debug.Log("mainscene");
    }

    void OnCollisionEnter(Collision collision) {
           Debug.Log("Collision Enter: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player") {
            // The enemy collided with the player, so end the game.
            SceneManager.LoadScene("ENDSCREEN"); // Replace "GameOverScene" with the name of your game over scene.
        }
    }
}
