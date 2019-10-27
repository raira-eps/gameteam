using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    [SerializeField] private Transform canvastransform;          //インスタンシエイトする際に階層を指定する
    [SerializeField] private Text pauseButtonText;               //ポーズボタンに表示するテキスト
    [SerializeField] private GameObject pauseUIPrefab;           //ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIInstance;                          //ポーズUIのインスタンス

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PauseClick()
    {
        //ポーズ画面を出す
        if (pauseUIInstance == null) {
            pauseButtonText.text = "Resume";
            pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            pauseUIInstance.transform.SetParent(canvastransform, false);
            Time.timeScale = 0f;
        }
        //ポーズ画面を消す
        else if (pauseUIInstance != null) {
            pauseButtonText.text = "ll";
            Destroy(pauseUIInstance);
            Time.timeScale = 1f;
        }
    }
}
