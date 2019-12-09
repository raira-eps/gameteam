using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//K.R
public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _evaluation;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _maxScore;
    [SerializeField] TextMeshProUGUI _clearTime;
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject scoreGround;
    [SerializeField] GameObject characterGround;
    [SerializeField] GameObject moruga;
    [SerializeField] GameObject asahi;

    string stageName1 = "Shibuya", stageName2 = "Akihabara";
    int maxScore;                                //そのステージでの最高スコアを入れる変数
    float angle = 0;                            //スコアテキストに当たるライトの角度
    float startTime = 0.0f;
    float time;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
        moruga.SetActive(false);
        moruga.GetComponent<Animator>().enabled = false;
        asahi.SetActive(false);
        asahi.GetComponent<Animator>().enabled = false;

        if (PlayerPrefs.GetInt("chara") == 1) moruga.SetActive(true);
        else if (PlayerPrefs.GetInt("chara") == 2) asahi.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(Ground());
    }

    void FixedUpdate()
    {
        angle += 0.02f;
        _score.materialForRendering.SetFloat("_LightAngle", angle);
        if (angle >= 6.28f) angle = 0;

        time = (Time.time - startTime) / 7.0f;
    }

    //スコアの評価基準
    string DivideEvaluation(int score) 
    {
        score = score / 500;
        switch (score) {
            case 0:
                return "F";
            case 1:
                return "E";
            case 2:
                return "D";
            case 3:
                return "C";
            case 4:
                return "B";
            case 5:
                return "A";
            default:
                return "S";
        }
    }

    //同じステージに遷移
    public void Retry()
    {
        if (StageSelect.StageName == stageName1) {
            SceneManager.LoadScene(4);
        }else if(StageSelect.StageName == stageName2) {
            SceneManager.LoadScene(5);
        }
    }

    //ステージセレクトへ遷移
    public void GoStageSelect() => SceneManager.LoadScene(2);

    IEnumerator Ground()
    {
        scoreGround.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        characterGround.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetInt("chara") == 1) moruga.GetComponent<Animator>().enabled = true;
        else if (PlayerPrefs.GetInt("chara") == 2) asahi.GetComponent<Animator>().enabled = true;

        StartCoroutine(Text());
    }

    //スコア等の表示
    IEnumerator Text()
    {
        //自己ベストを更新した時の処理
        if (maxScore < gameManager.score) {
            maxScore = gameManager.score;
            PlayerPrefs.SetInt($"{StageSelect.StageName}", maxScore);
        }

        maxScore = PlayerPrefs.GetInt($"{StageSelect.StageName}", maxScore);

        _clearTime.text = $"{gameManager.minutes.ToString("00")} : {gameManager.second.ToString("0.<size=20>0</size>")}";
        yield return new WaitForSeconds(0.5f);
        startTime = Time.time;
        float value = curve.Evaluate(time);
        _score.text = $"{(int)Mathf.Lerp(0, gameManager.score, value)}";
        _maxScore.text = $"{(int)Mathf.Lerp(0, maxScore, value)}";
        yield return new WaitForSeconds(10f);
        _evaluation.text = $"{DivideEvaluation(gameManager.score)}";                  //スコア評価の表示
    }
}
