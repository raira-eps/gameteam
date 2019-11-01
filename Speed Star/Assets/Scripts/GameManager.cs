using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    protected static readonly string[] findtags = { "GameManager", };

    /* -- Score (ゲーム中のスコアを入れる) ---------------------------------------------------------- */
    public int Score { set { score = value; } get { return score; }  }
    int score = 0;

    /* -- TrickCount (ゲーム中のトリックをした回数) ------------------------------------------------ */
    public int TrickCount { set { trickCount = value; } get { return trickCount; } }
    int trickCount = 0;

    /* -- Time (ゲーム中時間) ------------------------------------------------------------------------ */
    public int Time { set { time = value; } get { return time; } }
    int time = 0;

    /* -- Horizontal入力 ------------------------------------------------------------------------------ */
    public float MoveKey { get { return moveKey; } }
    readonly float moveKey = 1;

    /* -- Jump入力 ----------------------------------------------------------------------------------- */
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

    void Awake()
    {
        CheckInstance();
    }

    private bool CheckInstance()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        } else if (Instance == this)
            return true;

        Destroy(this);
        return false;
    }

    void Update()
    {
        //ジャンプ
        if (Input.GetMouseButtonDown(0)) jumpKey = 2;
    }
}
