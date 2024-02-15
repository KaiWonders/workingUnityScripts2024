using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnDestroy : MonoBehaviour
{
    public GameObject toChangeStateOf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DespawnObject(){
        // Destroy the GameObject
        Destroy(toChangeStateOf);
    }

    public void ToggleOnOff(){
        toChangeStateOf.SetActive (!toChangeStateOf.activeInHierarchy); // swaps to the opposite state
        Debug.Log ("toggle on");
    }

}
