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

    void Update()
    {
        if (!areaJump) transform.position = new Vector3(playerPosition.position.x, transform.position.y, transform.position.z);
        else transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, transform.position.z);
    }
}
