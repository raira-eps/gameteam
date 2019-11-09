using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class CameraManager : MonoBehaviour
{
    Transform playerPosition;
    public static bool areaJump = false;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (!areaJump) transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(9.0f, 2.0f, -10.0f), 5.0f * Time.deltaTime);
        else transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(5.0f, 2.0f, -10.0f), 20.0f * Time.deltaTime);
    }
}
