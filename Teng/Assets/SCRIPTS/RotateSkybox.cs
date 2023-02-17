using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    // Float variable
    public float RotateSky=5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotating the skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSky);
    }
}
