using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceDetection : MonoBehaviour
{
    public Transform playerTransform; // Drop the player into this in editor
    public float detectionRange = 5f;
    public bool playerInRangeSphere;


    void Update()
    {
        // Calculate the distance between this object and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            playerInRangeSphere = true;
            //Debug.Log("Player in Range - detection sphere");
        }
        else
        {
            playerInRangeSphere = false;
            //Debug.Log("Player out of Range - detection sphere ");
        }

    }
}
