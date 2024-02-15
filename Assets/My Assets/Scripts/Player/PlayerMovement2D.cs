using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ANIMATOR BROKEN

public class PlayerMovement2D : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    
    public Rigidbody2D rb; 
    public Animator animator; // this is broken

    Vector2 movement;

    // Update is called once per frame
   void Update()
{

    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized; 


    if (movement != Vector2.zero)
    {
         animator.SetFloat("Horizontal", movement.x);
         animator.SetFloat("Vertical", movement.y);
     }

     animator.SetFloat("Speed", movement.sqrMagnitude);
}

    void FixedUpdate()
    {
    
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
