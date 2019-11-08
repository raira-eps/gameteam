using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    protected static readonly string[] findtags = { "GameManager", };

    public GameObject AirFenceMark;

    /* -- Score (ゲーム中のスコアを入れる) ---------------------------------------------------------- */
    public int Score { set; get; } = 0;

    /* -- TrickCount (ゲーム中のトリックをした回数) ------------------------------------------------ */
    public int TrickCount { set; get; } = 0;

    /* -- Time (ゲーム中時間) ------------------------------------------------------------------------ */
    public int Time { set; get; } = 0;

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
        Score = 0;
        TrickCount = 0;
        Time = 0;
        AirFenceMark.SetActive(false);
        Player.Create();
    }

    void Update()
    {
        //ジャンプ
        if (Input.GetMouseButtonDown(0)) {
            jumpKey = 2;
        }
    }

    public void AirMark()
    {
        StartCoroutine(AirMarkFlashing());
    }

    IEnumerator AirMarkFlashing()
    {
        for (int i = 0; i <= 2; i++) {
            AirFenceMark.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            AirFenceMark.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
}
