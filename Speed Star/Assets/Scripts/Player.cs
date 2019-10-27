
/*
 確認してほしい内容があるときは　*確認　をつけてPUSH!!
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float jampforce;            //飛ぶ力
    [SerializeField] float DownSpeed;            //フェンスにぶつかった時に下がる速度
    [SerializeField] float UpSpeed;　　　　　　  //フェンスにぶつかった時に上がる速度
    [SerializeField] float SpeedUpTime;          //スピードが上がっている時間
    [SerializeField] float SpeedDownTime;        //スピードが下がっている時間
    [SerializeField] int Score;                  //スコアを入れる変数
    [SerializeField] int GetTip;                 //Tipを獲得した時のスコア獲得値
    [SerializeField] private int haveTips;       //所持しているTip(テスト用に外から枚数を変更させる)
    Rigidbody rb;                                //プレイヤーのリジットボディー
    Slider speed_slider;                         //Debug用のスピード調整スライダー
    bool jamp = true;                            //ジャンプ中かどうかの判定
    float speed;                                 //移動速度
    float NormalSpeed;                           //最初のスピードの数値

    Vector3 offset;
    Vector3 target;
    float deg = 60;

    void Awake()
    {
        speed_slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        NormalSpeed = speed;
        CheckTip("Default");
    }

    void Update()
    {
        rb.velocity = new Vector3(NormalSpeed, 0, 0);  //プレイヤーの通常時の移動
        if (jamp) {
            if (0 < Input.touchCount)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    StartCoroutine(Jamp());

            if (Input.GetMouseButtonDown(0))
                StartCoroutine(Jamp());
        }
        Debug.Log(NormalSpeed);
    }

    void CheckTip(string tipevent)  //Tipが増減する際に呼ぶ関数
    {
        switch (tipevent)
        {
            case "GetTip":
                haveTips++;
                break;
            default:
                break;
        }

        if (haveTips < 10)
        {
            NormalSpeed = speed;
        }
        else if (haveTips >= 10 && haveTips < 20)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.1f;
        }
        else if (haveTips >= 20 && haveTips < 30)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.2f;
        }
        else if (haveTips >= 30 && haveTips < 40)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.3f;
        }
        else if (haveTips >= 40 && haveTips < 50)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.4f;
        }
        else if (haveTips >= 50)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.5f;
        }
    }

    void OnCollisionStay(Collision collision) => jamp = true;

    void OnCollisionExit(Collision collision) => jamp = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal") SceneManager.LoadScene(5);

        if (other.tag == "FenceRed") StartCoroutine(ChangeSpeed("Red", SpeedDownTime));        //ショートフェンスに触れたとき
        else if (other.tag == "FenceBlue") StartCoroutine(ChangeSpeed("Blue", SpeedUpTime));   //ブーストフェンスに触れたとき

        // Tipを獲得した時の処理。
        if (other.tag == "Tip")
        {
            Score = GameManager.Score;
            Score += GetTip;
            GameManager.Score = Score;
            CheckTip("GetTip");
        }

        //大ジャンプの処理
        if (other.tag == "AreaJump") {
            offset = transform.position;
            target = other.transform.GetChild(0).transform.position - offset;
            StartCoroutine(AreaJump());
        }
    }

    //Debug用のスライダーを動かしたときに呼ばれる関数
    public void Speed()
    {
        speed = speed_slider.value;
        NormalSpeed = speed;
    }

    //プレイヤーのジャンプの時に呼ばれる関数
    IEnumerator Jamp()
    {
        //飛んでる時の処理
        for (int i = 0; i < 20; i++)
            yield return rb.velocity = new Vector3(speed, jampforce, 0);
        
        //落ちる時の処理
        while (!jamp)
            yield return rb.velocity = new Vector3(speed, -jampforce / 2, 0);
    }

    //大ジャンプするときに呼ばれる関数
    IEnumerator AreaJump()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= target.x; x += 0.3f) {
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
            yield return null;
        }
    }

    //スピードが変わった時に呼ばれる関数
    IEnumerator ChangeSpeed(string speedname, float time)
    {
        if(speedname == "Red") NormalSpeed /= DownSpeed;
        if(speedname == "Blue") NormalSpeed *= UpSpeed;

        yield return new WaitForSeconds(time);
        NormalSpeed = speed;
        CheckTip("EffectCancel");
    }
}
