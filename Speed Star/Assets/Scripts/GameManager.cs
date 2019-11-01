using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//K.R
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    protected static readonly string[] findtags = { "GameManager", };
    static public int Score;                     //ゲーム中のスコアを入れる
    static public int TrickCount;                //ゲーム中のトリックをした回数
    static public int Time;                      //ゲーム中時間

    /* -- Horizontal入力 --------------------------------------------------------------------------- */
    public float MoveKey { get { return moveKey; } }
    readonly float moveKey = 1;

    /* -- Jump入力 --------------------------------------------------------------------------------- */
    public int JumpKey { get { return jumpKey; } }
    public int jumpKey = 0;

    public static GameManager Instance
    {
        get {
            if (instance == null) {
                Type type = typeof(GameManager);

                foreach (var tag in findtags) {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                    for (int j = 0; j < objs.Length; j++) {
                        instance = (GameManager)objs[j].GetComponent(type);
                        if (instance != null) return instance;
                    }
                }

                Debug.LogWarning(string.Format("{0} is not found", type.Name));
            }

            return instance;
        }
    }

    void Update()
    {
        //ジャンプ
        if (Input.GetMouseButtonDown(0)) jumpKey = 2;
    }
}
