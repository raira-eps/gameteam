using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class CameraManager : MonoBehaviour
{
    Vector3 cameraVector;
    Transform playerPosition;
    public static bool areaJump = false;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        cameraVector = new Vector3(10.0f, 2.0f, -10.0f);
    }

    void FixedUpdate()
    {
        if (!areaJump) {
            transform.position = Vector3.Lerp(transform.position, playerPosition.position + cameraVector, 5.0f * Time.deltaTime);
        }
        else transform.position = Vector3.Lerp(transform.position, playerPosition.position + cameraVector, 20.0f * Time.deltaTime);
    }
}
