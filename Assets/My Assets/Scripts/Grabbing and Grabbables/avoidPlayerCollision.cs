using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidPlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
     if (other.gameObject.CompareTag("AAA")){ //make this tag whatever u want to avoid collisions w
        Collider collOther = other.gameObject.GetComponent<Collider>();
        Collider collThis = GetComponent<Collider>();
        Physics.IgnoreCollision(collOther,collThis);
     }
    }
}
