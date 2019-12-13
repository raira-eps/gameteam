using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class CameraManager : MonoBehaviour
{
    static public Transform playerPosition;
    public static bool areaJump = false;

    void FixedUpdate()
    {
        if (playerPosition != null) {
            if (!areaJump) transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(9.0f, 2.0f, -10.0f), 5.0f * Time.deltaTime);
            else transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(5.0f, 2.0f, -10.0f), 20.0f * Time.deltaTime);
        }
    }

    static public void Find()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
