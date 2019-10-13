using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;                //移動速度
    [SerializeField] float jampforce;            //飛ぶ力
    Rigidbody rb;
    bool jamp　= true;                                   //ジャンプ中かどうかの判定

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector3(speed, 0, 0);  //プレイヤーの通常時の移動

        if (jamp) {
            //if (0 < Input.touchCount)
            //    if (Input.GetTouch(0).phase == TouchPhase.Began)
            //        StartCoroutine(Jamp());

            if (Input.GetMouseButtonDown(0))
                StartCoroutine(Jamp());
        }
    }

    void OnCollisionStay(Collision collision)
    {
        jamp = true;
    }

    void OnCollisionExit(Collision collision)
    {
        jamp = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal") SceneManager.LoadScene(5);
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
}
