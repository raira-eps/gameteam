using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    //インスタンシエイトする際に階層を指定する
    [SerializeField] private Transform canvastransform;

    [SerializeField] private Text pauseButtonText;
    //　ポーズした時に表示するUIのプレハブ
    [SerializeField] private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PauseClick()
    {
        if (pauseUIInstance == null)
        {
            pauseButtonText.text = "Resume";
            pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            pauseUIInstance.transform.SetParent(canvastransform, false);
            Time.timeScale = 0f;
        }
        else if (pauseUIInstance != null)
        {
            pauseButtonText.text = "ll";
            Destroy(pauseUIInstance);
            Time.timeScale = 1f;
        }
    }
}
