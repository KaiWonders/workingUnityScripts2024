using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billtest : MonoBehaviour
{
    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
