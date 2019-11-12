using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//K.R
public class Result : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] TextMeshProUGUI _evaluation;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _maxScore;
    [SerializeField] TextMeshProUGUI _clearTime;

    int maxScore;                                //そのステージでの最高スコアを入れる変数
    int trickCount;                              //ランシーンから送られてくるトリックカウントを入れる変数
    float angle = 0;                            //スコアテキストに当たるライトの角度

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        //自己ベストを更新した時の処理
        if (maxScore < gameManager._score) {
            maxScore = gameManager._score;
            PlayerPrefs.SetInt($"{StageSelect.StageName}", maxScore);
        }

        maxScore = PlayerPrefs.GetInt($"{StageSelect.StageName}", maxScore);
        trickCount = gameManager.trickCount;

        _evaluation.text = $"{DivideEvaluation(gameManager._score)}";                  //スコア評価の表示
        _score.text = $"{gameManager._score}";
        _maxScore.text = $"{maxScore}";
        _clearTime.text = $"{gameManager._time / 60} : {gameManager._time % 60}";
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            _panel.SetActive(true);
#else
         if (0 < Input.touchCount)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                _panel.SetActive(true);
#endif
    }

    void FixedUpdate()
    {
        angle += 0.02f;
        _score.materialForRendering.SetFloat("_LightAngle", angle);
        if (angle >= 6.28f) angle = 0;
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
    public void Retry() => SceneManager.LoadScene(4);

    //ステージセレクトへ遷移
    public void GoStageSelect() => SceneManager.LoadScene(2);
}
