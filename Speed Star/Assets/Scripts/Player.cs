
/*
 確認してほしい内容があるときは　*確認　をつけてPUSH!!
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//K.R
public class Player : MonoBehaviour
{
    [SerializeField] float changeTimeSpeed;                     //スピードが元に戻るまでの時間
    [SerializeField, Range(0, 50)] float downSpeed;         //フェンスにぶつかった時に下がる速度
    [SerializeField, Range(0, 50)] float upSpeed;             //フェンスにぶつかった時に上がる速度
    Rigidbody rb;                                                         //プレイヤーのリジットボディー
    GameObject countDownStart;
    GameObject trick;
    GameObject trickGauge;
    GameObject shortEffect;
    GameObject shortEffect2;
    GameObject boostEffect;
    GameObject boostEffect1;
    GameObject boostEffect2;
    GameObject boostEffect3;
    GameObject boostEffect4;
    Animator countDown;
    Animator animator;

    Vector3 offset;
    Vector3 airOffset;                                      //エアフェンス飛び初めの位置
    Vector3 target;
    Vector3 airTarget;                                     //エアフェンス着地点
    Vector3 FirstPos;                                      //計算用の変数
    Vector3 PlyPos;
    Vector3 AirPos;

    bool isGrounded = true;                            //床についてるかどうかの判定
    bool isJumping = false;                             //ジャンプ中かどうかの判定
    bool jumpingCheck = true;                       //ジャンルできるかどうかの判定
    bool isFenceTime = false;                         //フェンスの効果時間かどうか
    bool isAir = false;                                    //エアフェンスのイベントを判定する
    bool isAirJump = false;                            //エアフェンスのジャンプの判定
    bool isAirTiming = false;
    static bool trickcheck = false;
    static public bool IsArrival = false;
    string fence;                                           //当たったフェンスの名前
    static float moveSpeed;                                    //プレイヤーのスピード
    static float t_speed;
    float speed;
    float timer = 0;
    float jumpTimeCounter;
    float jumpTime = 0.35f;
    float jumpPower;
    float deg = 60;                                        //大ジャンプするときの初角度
    float airTime;
    float JumpTiming;
    float JumpFinish;
    static float m_speed;
    static public float _trickMoveSpeed = 0.15f;
    int JumpCount;
    int count;
    static public int getTip;                                         //Tipを獲得した時のスコア獲得値
    static public int haveTips;                                     //所持しているTip

    bool isCount;
    bool isInput;

    GameManager gameManager;
    PlayerManager playerManager;

    static public Player Create()
    {
        var pos = GameObject.FindGameObjectWithTag("Start").transform.position;
        Player p;
        if (PlayerPrefs.GetInt("chara") == 1) p = Resources.Load<Player>("Prefabs/Moruga");
        else p = Resources.Load<Player>("Prefabs/Asahi");
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
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        shortEffect = (GameObject)Resources.Load("Prefabs/ShortEffect");
        shortEffect2 = (GameObject)Resources.Load("Prefabs/ShortEffect2");
        countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<Animator>();
        countDown.SetBool("Count", false);
        IsArrival = false;

        // 使っているキャラの Effectを呼び出す。
        if (PlayerPrefs.GetInt("chara") == 1)
        {
            boostEffect = (GameObject)Resources.Load("Prefabs/Moruga_Effect");
            boostEffect1 = (GameObject)Resources.Load("Prefabs/Moruga_Effect1");
            boostEffect2 = (GameObject)Resources.Load("Prefabs/Moruga_Effect2");
            //boostEffect3 = (GameObject)Resources.Load("Prefabs/Moruga_Effect3");
            //boostEffect4 = (GameObject)Resources.Load("Prefabs/Moruga_Effect4");
        }
        else
        {
            boostEffect = (GameObject)Resources.Load("Prefabs/Asahi_Effect");
            boostEffect1 = (GameObject)Resources.Load("Prefabs/Asahi_Effect1");
            boostEffect2 = (GameObject)Resources.Load("Prefabs/Asahi_Effect2");
            //boostEffect3 = (GameObject)Resources.Load("Prefabs/AsahiEffect3");
            //boostEffect4 = (GameObject)Resources.Load("Prefabs/AsahiEffect4");
        }

        trick = GameObject.FindGameObjectWithTag("Trick");
        trickGauge = GameObject.FindGameObjectWithTag("TrickGauge");
        trick.SetActive(false);
        trickGauge.SetActive(false);
        getTip = 0;
    }

    void Start()
    {
        moveSpeed = playerManager.MoveSpeed;
    }

    void Update()
    {
        gameManager.tip = haveTips;
        gameManager.score = getTip;
        if (TrickData.c == 1 || TrickData.c == 2)
            animator.SetBool("Trick", true);
        else
            animator.SetBool("Trick", false);
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && isInput)
        {
            isInput = false;
            isAirJump = true;
            isAirTiming = true;
            animator.SetBool("isAir", true);
           
        }
        //animator.SetBool("isAir", false);
#else
                if (Input.touchCount == 2){
                    isAirJump = true;
                    isAirTiming = true;
                }
#endif
    }

    void FixedUpdate()
    {
        Jump(); 
        if (isFenceTime) SpeedReset(changeTimeSpeed, speed);
        if (trickcheck) TrickSpeed(3, m_speed);
        if (isAir)
        {
            airTime += Time.deltaTime;
            if (airTime > 1.6 - 0.15) {
                if (airTime < 1.6 + 0.15) {
                    isInput = true;
                }
            }
            if (airTime > 2.5) {
                AudioManeger.SoundSE(AudioManeger.SE.ButSE);
                airTime = 0.0f;
            }
        }

        if (isAirJump == true && isAirTiming == true) {
            AudioManeger.SoundSE(AudioManeger.SE.SucusseSE);
            PlyPos = transform.position;
            FirstPos = airOffset - PlyPos;
            StartCoroutine(AirFenceJump(PlyPos, FirstPos,0.3f));
            animator.SetBool("JumpStart", true);
            isAirTiming = false;
        }
        animator.SetBool("isJump", isJumping);
        
    }

    void OnCollisionStay(Collision collision) => isGrounded = true;

    void OnCollisionExit(Collision collision) => isGrounded = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            IsArrival = true;
        }

        if (other.tag == "ShortFence") {
            CheckTip("Effect");
            ChangeSpeed("ShortFence");        //ショートフェンスに触れたとき
            AudioManeger.SoundSE(AudioManeger.SE.ShortSE);
            Instantiate(shortEffect, Vector3.zero, Quaternion.identity, transform);
            Instantiate(shortEffect2, Vector3.zero, Quaternion.identity, transform);
        }
        else if (other.tag == "BoostFence") {
            CheckTip("Effect");
            ChangeSpeed("BoostFence");   //ブーストフェンスに触れたとき
            animator.SetBool("isBoost", true);
            Invoke("Boost", 0.5f);
            AudioManeger.SoundSE(AudioManeger.SE.BoostSE);
            Instantiate(boostEffect, Vector3.zero, Quaternion.identity, transform);
            Instantiate(boostEffect1, Vector3.zero, Quaternion.identity, transform);
            Instantiate(boostEffect2, Vector3.zero, Quaternion.identity, transform);
            
        }

        // Tipを獲得した時の処理。
        if (other.tag == "Tip") {
            haveTips += 1;
            CheckTip("GetTip");
            AudioManeger.SoundSE(AudioManeger.SE.TipsSE);
        }

        //エリアジャンプの処理
        if (other.tag == "AreaJump") {
            animator.SetBool("JumpStart", true);
            CameraManager.areaJump = true;
            offset = transform.position;
            target = other.transform.GetChild(0).transform.position - offset;
            Furic._trickcheck = true;
            trick.SetActive(true);
            trickGauge.SetActive(true);
            StartCoroutine(AreaJump());
        }
        //エアフェンスまでの処理を始める　制作山藤
        if (other.tag == "AirFenceEvent") {
            isCount = true;
        }

        //エアフェンスの処理　制作山藤
        if (other.tag == "AirFence" && isAirJump == true) {
            StartCoroutine(AirFenceJump(airOffset, airTarget,0.7f));
            count = 1;
            isAirJump = false;
            airTime = 0;
        }
        if (other.tag == "AirFencePos")
        {
            isAir = false;
            if (isInput)
            {
                isInput = false;
            }
        }

        //バナナ　制作　山藤
        if (other.tag == "Banana")
        {
            gameManager.tip = 0;  //UIチップの表記の方
            haveTips = 0;         //スピードの方チップ
            CheckTip(default);
            AudioManeger.SoundSE(AudioManeger.SE.BananaSE);
            AudioManeger.VoiceSE(AudioManeger.Voice.Damage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //カウントダウン処理
        if (other.tag == "Count") {
            float target = Vector2.Distance(other.transform.GetChild(0).transform.position, transform.position);
            if (target <= moveSpeed * 3) {
                countDown.SetBool("Count", true);
                GameManager._isTrick = false;
            }
        }

        if (other.tag == "AirFenceEvent")
        {
            airOffset = other.transform.GetChild(1).transform.position;
            airTarget = other.transform.GetChild(0).transform.position - airOffset;
            float target = Vector2.Distance(other.transform.GetChild(2).transform.position , transform.position);
            if (target <= moveSpeed * 1.6f && isCount == true)
            {
                StartCoroutine(gameManager.AirMark(0.4f));
                isCount = false;
                isAir = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Goal")
        {
            AudioManeger.SoundSE(AudioManeger.SE.GoalSE);
            AudioManeger.VoiceSE(AudioManeger.Voice.Goal);
            Invoke("EndRunScene", 2);
        }
    }

    void Boost()
    {
        animator.SetBool("isBoost", false);
    }

    void EndRunScene()
    {
        SceneManager.LoadScene(6);
    }

    void Jump()
    {
        if (isGrounded) {      //ジャンプできるかどうか？
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
            if (jumpingCheck && gameManager.jumpKey != 0) {
                jumpTimeCounter = jumpTime;
                jumpingCheck = false;
                isJumping = true;
                jumpPower = playerManager.JumpPower;
                AudioManeger.VoiceSE(AudioManeger.Voice.Jump);
            }
        } else {
            if (gameManager.jumpKey == 0)
            {
                isJumping = false;
            }
            if (!isJumping) rb.velocity = new Vector3(moveSpeed, Physics.gravity.y * playerManager.GravityRate);
        }

        if (isJumping) {          //ジャンプ中かどうか？
            jumpTimeCounter -= Time.deltaTime;
            if (gameManager.jumpKey == 2) {
                jumpPower -= 0.2f;
                rb.velocity = new Vector3(moveSpeed, jumpPower);
            }
            if (jumpTimeCounter < 0) {
                isJumping = false;
                gameManager._jumpKey = 0;
            }
        }

        if (gameManager.jumpKey == 0) jumpingCheck = true;
    }

    static public void Trick(int t, int s, float trickspeed)
    {
        haveTips += t;
        getTip += s;
        m_speed = moveSpeed;
        moveSpeed = trickspeed;
        t_speed = trickspeed;
    }

    void TrickSpeed(float wait, float orignalSpeed)
    {
        timer += Time.deltaTime;
        moveSpeed -= (t_speed - orignalSpeed) / (wait * 50);

        if (timer >= wait) CheckTip("EffectCancel");
    }

    //S.Y制作
    void SpeedReset(float wait, float orignalSpeed)
    {
        timer += Time.deltaTime;
        if (fence == "ShortFence") moveSpeed -= (downSpeed - orignalSpeed) / (wait * 50);
        else if (fence == "BoostFence") moveSpeed -= (upSpeed - orignalSpeed) / (wait * 50);

        if (timer >= wait) CheckTip("EffectCancel");
    }

    //Tipが増減する際に呼ぶ関数
    void CheckTip(string tipevent)
    {
        switch (tipevent) {
            case "GetTip":
                break;
            case "Effect":
                isFenceTime = true;
                break;
            case "EffectCancel":
                timer = 0;
                moveSpeed = speed;
                isFenceTime = false;
                trickcheck = false;
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

    //エアフェンスからのジャンプ　制作山藤
    IEnumerator AirFenceJump(Vector3 Offset, Vector3 Target, float JumpTimeSpeed)
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (Target.y - b * Target.x) / (Target.x * Target.x);
        float a1 = Target.z / Target.x;

        for (float x = 0; x <= Target.x; x += JumpTimeSpeed) {
            yield return new WaitForFixedUpdate();
            float y = a * x * x + b * x;
            float z = a1 * x;
            transform.position = new Vector3(x, y, z) + Offset;
        }
        if (count == 1)
        {
            AudioManeger.SoundSE(AudioManeger.SE.Landing);
            animator.SetBool("isAir", false);
            count = 0;
        }
        CameraManager.areaJump = false;

    }

    //エリアジャンプするときに呼ばれる関数
    IEnumerator AreaJump()
    {
        AudioManeger.VoiceSE(AudioManeger.Voice.TrickJump);
        _trickMoveSpeed = 0.15f;
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= target.x; x += _trickMoveSpeed) {
            yield return new WaitForFixedUpdate();
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
        }
        animator.SetBool("JumpStart", false);
        countDown.SetBool("Count", false);
        CameraManager.areaJump = false;
        GameManager._isTrick = true;
        Furic._trickcheck = false;
        trick.SetActive(false);
        trickGauge.SetActive(false);
        gameManager._jumpKey = 0;
        trickcheck = true;
        AudioManeger.SoundSE(AudioManeger.SE.Landing);
    }
}
