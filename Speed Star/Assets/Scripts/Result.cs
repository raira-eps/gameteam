using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

//K.R
public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _evaluation;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _maxScore;
    [SerializeField] TextMeshProUGUI _clearTime;
    [SerializeField] TextMeshProUGUI _newRecord;
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject scoreGround;
    [SerializeField] GameObject characterGround;
    [SerializeField] GameObject moruga;
    [SerializeField] GameObject asahi;
    [SerializeField] GameObject retry;
    [SerializeField] GameObject stageSelect;
    [SerializeField] AudioClip numericalCalculation;
    [SerializeField] AudioClip scoreDisplay;
    [SerializeField] AudioClip BGM1;
    [SerializeField] AudioClip BGM2;
    [SerializeField] AudioClip s_Clear;
    [SerializeField] AudioClip Clear;
    [SerializeField] AudioClip Asahi_A;
    [SerializeField] AudioClip Asahi_B;
    [SerializeField] AudioClip Asahi_C;
    [SerializeField] AudioClip Moruga_A;
    [SerializeField] AudioClip Moruga_B;
    [SerializeField] AudioClip Moruga_C;
    [SerializeField] AudioMixerGroup Asahi;
    [SerializeField] AudioMixerGroup Moruga;
    /// <summary>
    /// 評価　magnification
    /// </summary>
    [SerializeField] private int timeMagnification = 0;//クリア時間による評価数値(レベルデザイン用）
                     private int timeBonus = 0; //実際にスコアから評価を出す数値
    /// <summary>
    /// 効果音
    /// </summary>
    AudioSource audioSourceSE;

    /// <summary>
    /// キャラのボイス
    /// </summary>
    AudioSource audioSourceVoice;

    /// <summary>
    /// リザルトBGM
    /// </summary>
    AudioSource audioSourceBGM;

    /// <summary>
    /// 獲得スコア毎のジングル
    /// </summary>
    AudioSource audioSourceBGM1;

    string stageName1 = "Shibuya", stageName2 = "Akihabara";
    int maxScore;                                //そのステージでの最高スコアを入れる変数
    float angle = 0;                            //スコアテキストに当たるライトの角度
    float startTime = 0.0f;
    float time;
    bool score;
    bool _isnew;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
        audioSourceSE = gameObject.GetComponents<AudioSource>()[0];
        audioSourceVoice = gameObject.GetComponents<AudioSource>()[1];
        audioSourceBGM = gameObject.GetComponents<AudioSource>()[2];
        audioSourceBGM1 = gameObject.GetComponents<AudioSource>()[3];

        moruga.SetActive(false);
        moruga.GetComponent<Animator>().enabled = false;
        asahi.SetActive(false);
        asahi.GetComponent<Animator>().enabled = false;
        score = false;
        _isnew = false;
        retry.SetActive(false);
        stageSelect.SetActive(false);
        audioSourceBGM1.PlayOneShot(DivideEvaluation(gameManager.score) == "S" ? s_Clear : Clear);
        Invoke("BGM", 3.5f);

        if (PlayerPrefs.GetInt("chara") == 1) {
            moruga.SetActive(true);
            audioSourceVoice.outputAudioMixerGroup = Moruga;
        }
        else if (PlayerPrefs.GetInt("chara") == 2) {
            asahi.SetActive(true);
            audioSourceVoice.outputAudioMixerGroup = Asahi;
        }
    }

    void Start()
    {
        StartCoroutine(Ground());
    }

    void FixedUpdate()
    {
        if (_isnew) {
            angle += 0.02f;
            _score.materialForRendering.SetFloat("_LightAngle", angle);
            if (angle >= 6.28f) angle = 0;
        }

        if(score) ScoreAnimation();
    }

    void BGM()
    {
        audioSourceBGM.PlayOneShot(DivideEvaluation(gameManager.score) == "S" ? BGM1 : BGM2);
    }

    void ScoreAnimation()
    {
        time = (Time.time - startTime);
        float value = curve.Evaluate(time);
        _score.text = $"{(int)Mathf.Lerp(0, gameManager.score, value)}";
        _maxScore.text = $"{(int)Mathf.Lerp(0, maxScore, value)}";
        if (_isnew) _newRecord.text = "New Record";
        if (curve.Evaluate(time) == 1.0f) {
            audioSourceSE.Stop();
            audioSourceSE.PlayOneShot(scoreDisplay);
            score = false;
        }
        else audioSourceSE.PlayOneShot(numericalCalculation);
    }

    //スコアの評価基準
    string DivideEvaluation(int score) 
    {
        //デフォルトの評価数値
        timeBonus = 500;
        
        //ゲームのクリアタイムが2分以内ならボーナスで評価
        if (gameManager.minutes < 2) timeBonus = timeMagnification;
        
        score = score / timeBonus;
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

        StartCoroutine(Text());
    }

    //スコア等の表示
    IEnumerator Text()
    {
        //自己ベストを更新した時の処理
        if (maxScore < gameManager.score) {
            _isnew = true;
            maxScore = gameManager.score;
            PlayerPrefs.SetInt($"{StageSelect.StageName}", maxScore);
        }

        maxScore = PlayerPrefs.GetInt($"{StageSelect.StageName}", maxScore);

        _clearTime.text = $"{gameManager.minutes.ToString("00")}:{gameManager.second.ToString("00.<size=60>0</size>")}";
        audioSourceSE.PlayOneShot(scoreDisplay);
        yield return new WaitForSeconds(0.5f);
        startTime = Time.time;
        score = true;
        yield return new WaitForSeconds(2.5f);
        _evaluation.text = $"{DivideEvaluation(gameManager.score)}";                  //スコア評価の表示
        audioSourceSE.PlayOneShot(scoreDisplay);
        if (PlayerPrefs.GetInt("chara") == 1) moruga.GetComponent<Animator>().enabled = true;
        else if (PlayerPrefs.GetInt("chara") == 2) asahi.GetComponent<Animator>().enabled = true;
        retry.SetActive(true);
        stageSelect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (PlayerPrefs.GetInt("chara") == 1) {
            switch (DivideEvaluation(gameManager.score)) {
                case "S":
                    audioSourceVoice.PlayOneShot(Moruga_A);
                    break;
                case "A":
                    audioSourceVoice.PlayOneShot(Moruga_B);
                    break;
                case "B":
                    audioSourceVoice.PlayOneShot(Moruga_B);
                    break;
                case "C":
                    audioSourceVoice.PlayOneShot(Moruga_B);
                    break;
                case "D":
                    audioSourceVoice.PlayOneShot(Moruga_C);
                    break;
                case "E":
                    audioSourceVoice.PlayOneShot(Moruga_C);
                    break;
                case "F":
                    audioSourceVoice.PlayOneShot(Moruga_C);
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefs.GetInt("chara") == 2) {
            switch (DivideEvaluation(gameManager.score)) {
                case "S":
                    audioSourceVoice.PlayOneShot(Asahi_A);
                    break;
                case "A":
                    audioSourceVoice.PlayOneShot(Asahi_B);
                    break;
                case "B":
                    audioSourceVoice.PlayOneShot(Asahi_B);
                    break;
                case "C":
                    audioSourceVoice.PlayOneShot(Asahi_B);
                    break;
                case "D":
                    audioSourceVoice.PlayOneShot(Asahi_C);
                    break;
                case "E":
                    audioSourceVoice.PlayOneShot(Asahi_C);
                    break;
                case "F":
                    audioSourceVoice.PlayOneShot(Asahi_C);
                    break;
                default:
                    break;
            }
        }
    }
}
