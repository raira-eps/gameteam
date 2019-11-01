using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//K.R
public class Result : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] TextMeshProUGUI _score;
    int Score;                                   //ランシーンから送られてくるスコアを入れる変数
    int MaxScore;                                //そのステージでの最高スコアを入れる変数
    int TrickCount;                              //ランシーンから送られてくるトリックカウントを入れる変数
    int Time;                                    //ランシーンから送られてくるタイムを入れる変数
    string StageName;                            //クリアしたステージの名前を入れる変数

    GameManager gameManager;

    void Awake()
    {
        Score = gameManager.Score;
        StageName = StageSelect.StageName;
        MaxScore = PlayerPrefs.GetInt($"{StageName}", MaxScore);
        TrickCount = gameManager.TrickCount;
        Time = gameManager.Time;

        //自己ベストを更新した時の処理
        if (MaxScore < Score) {
            MaxScore = Score;
            PlayerPrefs.SetInt($"{StageName}", MaxScore);
        }
    }

    void Start()
    {
        //スコアの表示
        _score.text = $@"Evaluation             {DivideEvaluation(Score)}
Score           {Score}
BestScore    {MaxScore}
BestTime       {Time / 60}:{Time}
TrickCount       {TrickCount}";
    }

    void Update()
    {
        if (0 < Input.touchCount)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                _panel.SetActive(true);

        if (Input.GetMouseButtonDown(0))
            _panel.SetActive(true);
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
        SceneManager.LoadScene(4);
    }

    //ステージセレクトへ遷移
    public void GoStageSelect()
    {
        SceneManager.LoadScene(2);
    }
}
