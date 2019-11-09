
/*
 確認してほしい内容があるときは　*確認　をつけてPUSH!!
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;                                                              //ちんぽろ

//K.R
public class Player : MonoBehaviour
{
    [SerializeField] int getTip;                                         //Tipを獲得した時のスコア獲得値
    [SerializeField] float changeTimeSpeed;                     //スピードが元に戻るまでの時間
    [SerializeField] int haveTips;                                     //所持しているTip(テスト用に外から枚数を変更させる)
    [SerializeField, Range(0, 50)] float downSpeed;         //フェンスにぶつかった時に下がる速度
    [SerializeField, Range(0, 50)] float upSpeed;             //フェンスにぶつかった時に上がる速度
    [SerializeField] float AirTap;  
    TextMeshProUGUI ScoreText;
    Rigidbody rb;                                            //プレイヤーのリジットボディー
    Vector3 offset;
    Vector3 AirOffset;
    Vector3 target;
    Vector3 AirTarget;
    Vector3 _AirPos;　　　　　　　　　　　   　 //エアフェンスのジャンプの位置
    Vector3 PlayerPos;                                    //プレイヤーの位置
    bool isGrounded = true;                            //床についてるかどうかの判定
    bool isJumping = false;                             //ジャンプ中かどうかの判定
    bool JumpingCheck = true;                       //ジャンルできるかどうかの判定
    bool isFenceTime = false;                         //フェンスの効果時間かどうか
    bool IsAir = false;                                    //エアフェンスのイベントを判定する
    bool IsAirJump = false;                            //エアフェンスのジャンプの判定
    string fence;
    float moveSpeed;                                    //プレイヤーのスピード
    float speed;
    float timer = 0;
    float jumpTimeCounter;
    float jumpTime = 0.35f;
    float jumpPower;
    float deg = 60;                                        //大ジャンプするときの初角度
    float AirTime;
    int score = 0;                                          //スコアを入れる変数
    GameObject AirPointFinish;
    GameObject AirPoint;                                  //エアフェンスジャンプポイント
    Text AirFenceText;                             //Canvasのエアフェンスのタイミング用]
    GameManager gameManager;
    PlayerManager playerManager;

    static public Player Create()
    {
        var pos = GameObject.FindGameObjectWithTag("Start").transform.position;
        var p = Resources.Load<Player>("Prefabs/Player");
        var ins = Instantiate(p);
        ins.transform.position = pos;
        return ins;
    }

    void Awake()
    {
        playerManager = PlayerManager.Instance;
        gameManager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        jumpTimeCounter = jumpTime;
        CheckTip("Default");
        ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        AirPoint = GameObject.FindGameObjectWithTag("AirFencePos");
        _AirPos = AirPoint.transform.position;
    }

    void Start() => moveSpeed = playerManager.MoveSpeed;

    void FixedUpdate()
    {
        if(isFenceTime) SpeedReset(changeTimeSpeed, speed);
        Jump();
        if (IsAir)AirFenceAction();
    }

    void OnCollisionStay(Collision collision) => isGrounded = true;

    void OnCollisionExit(Collision collision) => isGrounded = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal") SceneManager.LoadScene(5);

        if (other.tag == "ShortFence") {
            CheckTip("Effect");
            ChangeSpeed("ShortFence");        //ショートフェンスに触れたとき
        }
        else if (other.tag == "BoostFence") {
            CheckTip("Effect");
            ChangeSpeed("BoostFence");   //ブーストフェンスに触れたとき
        }

        // Tipを獲得した時の処理。
        if (other.tag == "Tip") {
            score += getTip;
            gameManager.Score = score;
            CheckTip("GetTip");
            ScoreText.text = " Score " + score.ToString();
        }

        //大ジャンプの処理
        if (other.tag == "AreaJump") {
            CameraManager.areaJump = true;
            offset = transform.position;
            target = other.transform.GetChild(0).transform.position - offset;
            StartCoroutine(AreaJump());
        }
        //エアフェンスまでの処理を始める　制作山藤
        if (other.tag == "AirFenceEvent") {
            _AirPos = AirPoint.transform.position;
            PlayerPos = transform.position;
            IsAir = true;
            StartCoroutine(gameManager.AirMark());
        }

        //エアフェンスの処理　制作山藤
        if (other.tag == "AirFence" && IsAirJump == true) AirFenceJump();
    }

    void Jump()
    {
        if (isGrounded) {      //ジャンプできるかどうか？
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
            if (JumpingCheck && gameManager.JumpKey != 0) {
                jumpTimeCounter = jumpTime;
                JumpingCheck = false;
                isJumping = true;
                jumpPower = playerManager.JumpPower;
            }
        } else {
            if (gameManager.JumpKey == 0) isJumping = false;
            if (!isJumping) rb.velocity = new Vector3(moveSpeed, Physics.gravity.y * playerManager.GravityRate);
        }

        if (isJumping) {          //ジャンプ中かどうか？
            jumpTimeCounter -= Time.deltaTime;
            if (gameManager.JumpKey == 2) {
                jumpPower -= 0.2f;
                rb.velocity = new Vector3(moveSpeed, jumpPower);
            }
            if (jumpTimeCounter < 0) {
                isJumping = false;
                gameManager.jumpKey = 0;
            }
        }

        if (gameManager.JumpKey == 0) JumpingCheck = true;
    }

    //S.Y制作
    void SpeedReset(float wait, float orignalSpeed)
    {
        timer += Time.deltaTime;
        if (fence == "ShortFence") moveSpeed -= (downSpeed - orignalSpeed) / (wait * 50);
        else if(fence == "BoostFence") moveSpeed -= (upSpeed - orignalSpeed) / (wait * 50);

        if (timer >= wait) CheckTip("EffectCancel");
    }

    //Tipが増減する際に呼ぶ関数
    void CheckTip(string tipevent)
    {
        switch (tipevent) {
            case "GetTip":
                haveTips++;
                break;
            case "Effect":
                isFenceTime = true;
                break;
            case "EffectCancel":
                timer = 0;
                moveSpeed = speed;
                isFenceTime = false;
                break;
            default:
                break;
        }

        if (haveTips < 10 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.0f;
        }
        else if (haveTips >= 10 && haveTips < 20 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.1f;
        }
        else if (haveTips >= 20 && haveTips < 30 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.2f;
        }
        else if (haveTips >= 30 && haveTips < 40 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.3f;
        }
        else if (haveTips >= 40 && haveTips < 50 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.4f;
        }
        else if (haveTips >= 50 && !isFenceTime)
        {
            moveSpeed = playerManager.MoveSpeed * 1.5f;
        }
    }

    //フェンスに当たった時に呼ばれる関数
    void ChangeSpeed(string speedname)
    {
        speed = moveSpeed; fence = speedname;
        if (speedname == "ShortFence") moveSpeed = downSpeed;
        else if (speedname == "BoostFence") moveSpeed = upSpeed;
    }

    //エアフェンスまでの処理　制作山藤
    void AirFenceAction()
    {
        if (transform.position.x + AirTap > _AirPos.x) {
            AirTime += Time.deltaTime;
            if (AirTime < 0.5) {
                if (Input.GetMouseButtonDown(0)) {
                    IsAirJump = true;
                }
            }
        }
    }

    //エアフェンスからのジャンプ　制作山藤
    void AirFenceJump()
    {
        Debug.Log("ジャンプ成功");
    }

    //大ジャンプするときに呼ばれる関数
    IEnumerator AreaJump()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= target.x; x += 0.3f) {
            yield return new WaitForFixedUpdate();
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
        }

        CameraManager.areaJump = false;
    }
}
