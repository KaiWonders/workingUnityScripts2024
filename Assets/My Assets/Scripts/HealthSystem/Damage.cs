using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{   //Assign this to the weapon in VR
    // And give the enemy the PlayerHealth Script
    public float damage;
    private PlayerHealth healthManager;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("BBB")){ //Make sure the thing you want to take damage has this tag
            healthManager.HealthChanged(damage);}

    }

}

