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
    TextMeshProUGUI score;
    [SerializeField] private GameObject pauseUI;           //ポーズした時に表示するUIのプレハブ
    float countTime = 0;
    int minutes = 0;
    TextMeshProUGUI timeText;
    TextMeshProUGUI tipText;
    public int _tip;

    /* -- Score (ゲーム中のスコアを入れる) ---------------------------------------------------------- */
    public int _score { set; get; } = 0;

    /* -- TrickCount (ゲーム中のトリックをした回数) ------------------------------------------------ */
    public int trickCount { set; get; } = 0;

    /* -- Time (ゲーム中時間) ------------------------------------------------------------------------ */
    public int _time { set; get; } = 0;

    /* -- Jump入力 ----------------------------------------------------------------------------------- */
    public int jumpKey { get { return _jumpKey; } }
    public int _jumpKey = 0;

    public static GameManager Instance
    {
        get {
            if (instance == null) {
                Type type = typeof(GameManager);

                foreach (var tag in findTags) {
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
        _score = 0;
        trickCount = 0;
        _time = 0;
        score = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        Player.Create();
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Text();
        Jump();
    }

    void Text()
    {
        score.text = _score.ToString() + " Point";
        tipText.text = _tip.ToString() + " Tips";
    }

    void Jump()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif

        if (Input.GetMouseButtonDown(0)) _jumpKey = 2;      //ジャンプ
    }

    public IEnumerator AirMark(float timing)
    {
        for (int i = 0; i <= 2; i++) {
            airFenceMark.SetActive(true);
            yield return new WaitForSeconds(timing);
            airFenceMark.SetActive(false);
            yield return new WaitForSeconds(timing);
        }
        yield return null;
    }

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

    // タイマーの処理 A.T
    void FixedUpdate()
    {
        countTime += Time.deltaTime;
        timeText.text = minutes.ToString() + ":" + countTime.ToString("0.<size=20>00</size>");

        if (countTime >= 60.0f)
        {
            minutes += 1;
            countTime = 0;
        }
    }
}
