using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

//K.R
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    protected static readonly string[] findTags = { "GameManager", };

    [SerializeField] GameObject airFenceMark;
    [SerializeField] GameObject pauseUI;           //ポーズした時に表示するUIのプレハブ
    TextMeshProUGUI scoreText;
    TextMeshProUGUI timeText;
    TextMeshProUGUI tipText;
    public int tip;

    /* -- Score (ゲーム中のスコアを入れる) ---------------------------------------------------------- */
    public int score { set; get; } = 0;

    /* -- minutes (ゲーム中時間) ------------------------------------------------------------------------ */
    public int minutes { set; get; } = 0;

    /* -- Second (ゲーム中時間) ------------------------------------------------------------------------ */
    public float second { set; get; } = 0;

    /* -- Jump入力 ----------------------------------------------------------------------------------- */
    public int jumpKey { get { return _jumpKey; } }
    public int _jumpKey = 0;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Type type = typeof(GameManager);

                foreach (var tag in findTags)
                {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                    for (int j = 0; j < objs.Length; j++)
                    {
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
        score = 0;
        second = 0;
        minutes = 0;
        Player.Create();
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Text();
        Jump();
    }

    // タイマーの処理 A.T
    void FixedUpdate()
    {
        second += Time.deltaTime;
        timeText.text = minutes.ToString() + ":" + second.ToString("0.<size=20>0</size>");

        if (second >= 60.0f)
        {
            minutes += 1;
            second = 0;
        }
    }

    void Text()
    {
        scoreText.text = score.ToString() + " Point";
        tipText.text = tip.ToString() + " Tips";
    }

    void Jump()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif
        if (Input.GetMouseButtonDown(0))
        {
            _jumpKey = 2;      //ジャンプ
        }
    }

    #region ポーズ処理
    //ポーズ画面を出す
    public void PauseOpen()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        //SceneManager.GetActiveScene().name で現在のシーンの名前を取得
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Retire()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        SceneManager.LoadScene(2);
    }

    //ポーズ画面を消す
    public void PauseClose()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode) => Time.timeScale = 1f;
    #endregion

    int c = 0;
    public IEnumerator AirMark(float time)
    {
        c += 1;
        if (c == 1)
        {
            for (int i = 0; i <= 2; i++)
            {
                AudioManeger.SoundSE(AudioManeger.SE.TrikcCountdownSE);
                airFenceMark.SetActive(true);
                yield return new WaitForSeconds(time);
                airFenceMark.SetActive(false);
                yield return new WaitForSeconds(time);
            }
        }
    }
}
