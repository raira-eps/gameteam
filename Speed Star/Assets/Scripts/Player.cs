
/*
 確認してほしい内容があるときは　*確認　をつけてPUSH!!
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//K.R
public class Player : MonoBehaviour
{
    [SerializeField] float jampforce;                  //飛ぶ力
    [SerializeField] float DownSpeed;               //フェンスにぶつかった時に下がる速度
    [SerializeField] float UpSpeed;　　　　　　  //フェンスにぶつかった時に上がる速度
    [SerializeField] float SpeedUpTime;            //スピードが上がっている時間
    [SerializeField] float SpeedDownTime;        //スピードが下がっている時間
    [SerializeField] int GetTip;                         //Tipを獲得した時のスコア獲得値
    [SerializeField] private int haveTips;           //所持しているTip(テスト用に外から枚数を変更させる)
    Rigidbody rb;                                           //プレイヤーのリジットボディー
    Slider speed_slider;                                  //Debug用のスピード調整スライダー

    Vector3 offset;
    Vector3 target;

    bool isGrounded = true;                            //床についてるかどうかの判定
    bool isJumping = false;                             //ジャンプ中かどうかの判定
    bool isJumpingCheck = true;                     //ジャンルできるかどうかの判定
    bool IsBuffTime = false;                            //フェンスの効果時間かどうか
    float speed;                                              //移動速度
    float jumpTimeCounter;
    float jumpTime = 0.35f;
    float jumpPower;
    float NormalSpeed;                                  //最初のスピードの数値
    float deg = 60;                                        //大ジャンプするときの初角度
    int Score = 0;                                          //スコアを入れる変数

    GameManager gameManager;
    PlayerManager playerManager;

    void Awake()
    {
        speed_slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        rb = GetComponent<Rigidbody>();
        jumpTimeCounter = jumpTime;
    }

    void Start()
    {
        playerManager = PlayerManager.Instance;
        gameManager = GameManager.Instance;
        NormalSpeed = speed;
        CheckTip("Default");
    }

    void FixedUpdate()
    {
        //rb.velocity = new Vector3(NormalSpeed, 0, 0);  //プレイヤーの通常時の移動
        if (isGrounded) {
            rb.velocity = new Vector3(gameManager.MoveKey * playerManager.MoveSpeed, rb.velocity.y);
            if (isJumpingCheck && gameManager.JumpKey != 0) {
                jumpTimeCounter = jumpTime;
                isJumpingCheck = false;
                isJumping = true;
                jumpPower = playerManager.JumpPower;
            }
        } else {
            if (gameManager.JumpKey == 0) isJumping = false;
            if (!isJumping) rb.velocity = new Vector3(gameManager.MoveKey * playerManager.JumpMoveSpeed, Physics.gravity.y * playerManager.GravityRate);
        }

        if (isJumping) {
            jumpTimeCounter -= Time.deltaTime;
            if (gameManager.JumpKey == 2) {
                jumpPower -= 0.2f;
                rb.velocity = new Vector3(gameManager.MoveKey * playerManager.JumpMoveSpeed, 1 * jumpPower);
            }
            if (jumpTimeCounter < 0) isJumping = false;
        }

        if (gameManager.JumpKey == 0) isJumpingCheck = true;
    }

    void CheckTip(string tipevent)  //Tipが増減する際に呼ぶ関数
    {
        switch (tipevent)
        {
            case "GetTip":
                haveTips++;
                break;
            case "Effect":
                IsBuffTime = true;
                break;
            case "EffectCancel":
                IsBuffTime = false;
                break;
            default:
                break;
        }

        if (haveTips < 10 && !IsBuffTime)
        {
            NormalSpeed = speed;
        }
        else if (haveTips >= 10 && haveTips < 20 && !IsBuffTime)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.1f;
        }
        else if (haveTips >= 20 && haveTips < 30 && !IsBuffTime)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.2f;
        }
        else if (haveTips >= 30 && haveTips < 40 && !IsBuffTime)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.3f;
        }
        else if (haveTips >= 40 && haveTips < 50 && !IsBuffTime)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.4f;
        }
        else if (haveTips >= 50 && !IsBuffTime)
        {
            NormalSpeed = speed;
            NormalSpeed *= 1.5f;
        }
    }

    void OnCollisionEnter(Collision collision) => gameManager.jumpKey = 0;
   
    void OnCollisionStay(Collision collision) => isGrounded = true;

    void OnCollisionExit(Collision collision) => isGrounded = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal") SceneManager.LoadScene(5);

        if (other.tag == "FenceRed") StartCoroutine(ChangeSpeed("Red", SpeedDownTime));        //ショートフェンスに触れたとき
        else if (other.tag == "FenceBlue") StartCoroutine(ChangeSpeed("Blue", SpeedUpTime));   //ブーストフェンスに触れたとき

        if (other.tag == "FenceRed") CheckTip("Effect");        //ショートフェンスに触れたとき
        else if (other.tag == "FenceBlue") CheckTip("Effect");   //ブーストフェンスに触れたとき

        // Tipを獲得した時の処理。
        if (other.tag == "Tip")
        {
            Score = gameManager.Score;
            Score += GetTip;
            gameManager.Score = Score;
            CheckTip("GetTip");
        }

        //大ジャンプの処理
        if (other.tag == "AreaJump") {
            offset = transform.position;
            target = other.transform.GetChild(0).transform.position - offset;
            StartCoroutine(AreaJump());
        }
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
