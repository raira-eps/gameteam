using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class CameraManager : MonoBehaviour
{
    static public Transform playerPosition;
    static public bool areaJump = false;
    static public bool _isTrick = false;
    float timer = 3;

    void FixedUpdate()
    {
        if (playerPosition != null) {
            if (!Player.IsArrival) {
                if (_isTrick) StartCoroutine(TrickCamera());
                else {
                    timer = 3;
                    if (!areaJump) transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(9.0f, 2.0f, -10.0f), 5.0f * Time.deltaTime);
                    else transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(5.0f, 2.0f, -10.0f), 10.0f * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator TrickCamera()
    {
        transform.position = Vector3.Lerp(transform.position, playerPosition.position + new Vector3(0f, 2.5f, -6.0f), 10.0f * Time.deltaTime);
        if (timer <= 0) {
            _isTrick = false;
            Player._trickMoveSpeed = 0.5f;
        }
        else timer -= Time.deltaTime;
        yield break;
    }

    static public void Find()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
