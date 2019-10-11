using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] TextMeshProUGUI _evaluation;
    [SerializeField] TextMeshProUGUI _score;
    int Score;                  //ランシーンから送られてくるはずのスコア（ランシーンができれば必要なくなる）
    int MaxScore;            //そのステージでの最高スコア
    int TrickCount;           //ランシーンから送られてくるはずのトリックカウント
    int Time;                   //ランシーンから送られてくるはずのタイム
    string StageName;     //クリアしたステージの名前（ステージセレクトの時に送られてくる）

    void Awake()
    {
        if (MaxScore < Score) {
            MaxScore = Score;
            PlayerPrefs.SetInt($"{StageName}", MaxScore);
        }
    }

    void Start()
    {
        DivideEvaluation(Score);
        _score.text = $@"Score           {Score}
BestScore    {MaxScore}
BestTime       {Time / 60}:{Time}
TrickCount       {TrickCount}";
    }

    void Update()
    {
        //if (0 < Input.touchCount)
        //    if (Input.GetTouch(0).phase == TouchPhase.Began) {
        //        _panel.SetActive(true);
        //    }

        if (Input.GetMouseButtonDown(0))
            _panel.SetActive(true);
    }

    //スコアの評価
    void DivideEvaluation(int score) 
    {
        if (score <= 500) _evaluation.text = "Evaluation             F";
        else if (score <= 1000) _evaluation.text = "Evaluation             E";
    }
}
