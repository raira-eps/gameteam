﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected static readonly string[] findTags = { "Player", };
    static PlayerManager instance;

    public static PlayerManager Instance
    {
        get {
            if (instance == null) {
                Type type = typeof(PlayerManager);

                foreach (var tag in findTags) {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                    for (int j = 0; j < objs.Length; j++) {
                        instance = (PlayerManager)objs[j].GetComponent(type);
                        if (instance != null) return instance;
                    }
                }

                Debug.LogWarning(string.Format("{0} is not found", type.Name));
            }

            return instance;
        }
    }

    /* -- 移動速度(地上) --------------------------------------------------------------------------- */
    [SerializeField, Range(0f, 20f)] float moveSpeed = 10f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    /* -- 移動速度(空中) --------------------------------------------------------------------------- */
    [SerializeField, Range(0f, 20f)] float jumpMoveSpeed = 8f;
    public float JumpMoveSpeed
    {
        get { return jumpMoveSpeed; }
        set { jumpMoveSpeed = value; }
    }

    /* -- ジャンプ力 ------------------------------------------------------------------------------- */
    [SerializeField, Range(0f, 40f)] float jumpPower = 20f;
    public float JumpPower
    {
        get { return jumpPower; }
        set { jumpPower = value; }
    }

    /* -- 重力倍率 --------------------------------------------------------------------------------- */
    [SerializeField, Range(0f, 10f)] float gravityRate = 1.8f;
    public float GravityRate
    {
        get { return gravityRate; }
        set { gravityRate = value; }
    }
}
