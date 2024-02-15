using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour {
    void OnCollision(Collision collision) {
           Debug.Log("Collision Enter: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player") {
            SceneManager.LoadScene("SampleScene");
        }
    }
}