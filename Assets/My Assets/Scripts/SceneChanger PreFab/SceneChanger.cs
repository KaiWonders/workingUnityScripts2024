using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessary to manage scenes


// Don't drag this into the editor of the OnClick() button dumbass
// Add this script as component to the button
// Then drag the button to its own OnClick() thing
// DON'T FORGET to name what scene you want to to go to 

public class SceneChanger : MonoBehaviour
{   
       // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    
    [SerializeField] // Should let you edit in editor
    public string scenePath; // this isn't really practical but it works
    // Just make sure the name in editor matches the name of the scene you want it to
    

    public void LoadScene(){
        SceneManager.LoadScene(scenePath); 
    }
}
