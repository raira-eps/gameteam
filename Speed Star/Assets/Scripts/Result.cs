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

    AudioSource audioSourceSE;
    AudioSource audioSourceBGM;
    AudioSource audioSourceBGM1;

    string stageName1 = "Shibuya", stageName2 = "Akihabara";
    int maxScore;                                //そのステージでの最高スコアを入れる変数
    float angle = 0;                            //スコアテキストに当たるライトの角度
    float startTime = 0.0f;
    float time;
    bool score;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
        audioSourceSE = gameObject.GetComponents<AudioSource>()[1];
        audioSourceBGM = gameObject.GetComponents<AudioSource>()[0];
        audioSourceBGM1 = gameObject.GetComponents<AudioSource>()[2];

        moruga.SetActive(false);
        moruga.GetComponent<Animator>().enabled = false;
        asahi.SetActive(false);
        asahi.GetComponent<Animator>().enabled = false;
        score = false;
        retry.SetActive(false);
        stageSelect.SetActive(false);
        audioSourceBGM1.PlayOneShot(DivideEvaluation(gameManager.score) == "S" ? s_Clear : Clear);
        Invoke("BGM", 3.5f);

        if (PlayerPrefs.GetInt("chara") == 1) {
            moruga.SetActive(true);
            audioSourceSE.outputAudioMixerGroup = Moruga;
        }
        else if (PlayerPrefs.GetInt("chara") == 2) {
            asahi.SetActive(true);
            audioSourceSE.outputAudioMixerGroup = Asahi;
        }
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

        _clearTime.text = $"{gameManager.minutes.ToString("00")}:{gameManager.second.ToString("00.<size=20>0</size>")}";
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
                    audioSourceSE.PlayOneShot(Moruga_A);
                    break;
                case "A":
                    audioSourceSE.PlayOneShot(Moruga_B);
                    break;
                case "B":
                    audioSourceSE.PlayOneShot(Moruga_B);
                    break;
                case "C":
                    audioSourceSE.PlayOneShot(Moruga_B);
                    break;
                case "D":
                    audioSourceSE.PlayOneShot(Moruga_C);
                    break;
                case "E":
                    audioSourceSE.PlayOneShot(Moruga_C);
                    break;
                case "F":
                    audioSourceSE.PlayOneShot(Moruga_C);
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefs.GetInt("chara") == 2) {
            switch (DivideEvaluation(gameManager.score)) {
                case "S":
                    audioSourceSE.PlayOneShot(Asahi_A);
                    break;
                case "A":
                    audioSourceSE.PlayOneShot(Asahi_B);
                    break;
                case "B":
                    audioSourceSE.PlayOneShot(Asahi_B);
                    break;
                case "C":
                    audioSourceSE.PlayOneShot(Asahi_B);
                    break;
                case "D":
                    audioSourceSE.PlayOneShot(Asahi_C);
                    break;
                case "E":
                    audioSourceSE.PlayOneShot(Asahi_C);
                    break;
                case "F":
                    audioSourceSE.PlayOneShot(Asahi_C);
                    break;
                default:
                    break;
            }
        }
    }
}
