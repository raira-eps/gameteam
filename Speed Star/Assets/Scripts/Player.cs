
/*
 確認してほしい内容があるときは　*確認　をつけてPUSH!!
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;                //移動速度
    [SerializeField] float jampforce;            //飛ぶ力
    [SerializeField] float DownSpeed;            //フェンスにぶつかった時に下がる速度
    [SerializeField] float UpSpeed;　　　　　　  //フェンスにぶつかった時に上がる速度
    [SerializeField] float SpeedUpTime;          //スピードが上がっている時間
    [SerializeField] float SpeedDownTime;        //スピードが下がっている時間
    [SerializeField] int   Score;                //スコアを入れる変数
    [SerializeField] int   GetTip;               //Tipを獲得した時のスコア獲得値
    Rigidbody rb;
    bool jamp　= true;                                   //ジャンプ中かどうかの判定
    float NormalSpeed;                                   //最初のスピードの数値


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NormalSpeed = speed;
    }

    void Update()
    {
        rb.velocity = new Vector3(NormalSpeed, 0, 0);  //プレイヤーの通常時の移動
        if (jamp) {
            if (0 < Input.touchCount)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    StartCoroutine(Jamp());
        }
    }

    void OnCollisionStay(Collision collision) => jamp = true;

    void OnCollisionExit(Collision collision) => jamp = false;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal") SceneManager.LoadScene(5);

        if(other.tag == "FenceRed") StartCoroutine(ChangeSpeed("Red", SpeedDownTime));        //ショートフェンスに触れたとき
        else if (other.tag == "FenceBlue") StartCoroutine(ChangeSpeed("Blue", SpeedUpTime));   //ブーストフェンスに触れたとき

        // Tipを獲得した時の処理。
        if (other.tag == "Tip") {
            Score = GameManager.Score;
            Score += GetTip;
            GameManager.Score = Score;
        }
    }

    //プレイヤーが飛んだ時の処理
    IEnumerator Jamp()
    {
        //飛んでる時の処理
        for (int i = 0; i < 20; i++)
            yield return rb.velocity = new Vector3(speed, jampforce, 0);
        
        //落ちる時の処理
        while (!jamp)
            yield return rb.velocity = new Vector3(speed, -jampforce / 2, 0);
    }

    //スピードを変える処理
    IEnumerator ChangeSpeed(string speedname, float time)
    {
        if(speedname == "Red") NormalSpeed /= DownSpeed;
        if(speedname == "Blue") NormalSpeed *= SpeedUpTime;

        yield return new WaitForSeconds(time);
        NormalSpeed = speed;
    }
}
