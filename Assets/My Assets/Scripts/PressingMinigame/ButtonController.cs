using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
// Visually shows clicks of button
{
    public KeyCode keytopress;
    private SpriteRenderer theSr;
    public Sprite defaultImage;
    public Sprite pressedImage;



    // Start is called before the first frame update
    void Start()
    {
        theSr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keytopress)){
            theSr.sprite = pressedImage; //if key is pressed, change to pressed image
        }

        if (Input.GetKeyUp(keytopress)){
            theSr.sprite = defaultImage; // if key is released, change to default
        }
    }
}
