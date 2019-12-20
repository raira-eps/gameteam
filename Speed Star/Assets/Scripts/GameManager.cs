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
    [SerializeField] GameObject _countDown;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI timeText;
    TextMeshProUGUI tipText;
    static public bool _isTrick = true;
    public int tip;
    float _startTime;

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
        score = 0;
        second = 0;
        minutes = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<TextMeshProUGUI>();
        _countDown.SetActive(true);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("chara") == 1)
        {
            AudioManeger.SoundBGM(AudioManeger.BGM.Moruga);
        }
        else if (PlayerPrefs.GetInt("chara") == 2)
        {
            AudioManeger.SoundBGM(AudioManeger.BGM.Asahi);
        }
        StartCoroutine(CountDown());
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
        timeText.text = minutes.ToString("00") + ":" + second.ToString("00.<size=20>0</size>");

        if (second >= 60.0f) {
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
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif
        if (Input.GetMouseButtonDown(0)) {
            if(_isTrick) _jumpKey = 2;      //ジャンプ
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

    public IEnumerator AirMark(float time)
    {
        for (int i = 0; i <= 2; i++) {
            yield return new WaitForFixedUpdate();
            AudioManeger.SoundSE(AudioManeger.SE.TrikcCountdownSE);
            airFenceMark.SetActive(true);
            yield return new WaitForSeconds(time);
            airFenceMark.SetActive(false);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator CountDown()
    {
        for (int i = 0; i <= 4; i++) {
            switch (i) {
                case 4:
                    Player.Create();
                    CameraManager.Find();
                    _startTime = Time.deltaTime;
                    second = 0;
                    Destroy(_countDown);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
