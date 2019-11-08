using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //イニシャルY_Y
    [SerializeField] GameObject TitleText;
    [SerializeField] GameObject Start_text;
    [SerializeField] GameObject ClegitMenu;
    [SerializeField] GameObject ClegitIcon;
    [SerializeField] AudioSource TitleBGM;
    [SerializeField] AudioClip Sound1;
    [SerializeField] AudioClip Sound2;

    private float flash = 2.0f;
    private float time = 0.0f;
    private bool flashtime = true;
    //今のスクリプトだとスタートテキストが表示しているときにクレジットメニューを表示させるとレイヤーの関係上上に表示されてしまうの防ぐため使用する
    private int icon = 0;

    [SerializeField] string StageName1, StageName2; //テスト用に使用する

    void Start()
    {
        //ここでステージ１とステージ２の最高スコアを初期化しているが本来はこんな風に書きません！！（テスト用です）
        PlayerPrefs.SetInt("Shibuya", 0);
        PlayerPrefs.SetInt("Akihabara", 0);
        PlayerPrefs.SetInt("chara", 0);
        //TitleBGM.Play();
    }

    //スタートテキストを表示させたり非表示にさせるための処理
    void Update()
    {
        time += Time.deltaTime;
        if(time >= flash && icon== 0)
        {
            if(flashtime == true)
            {
                Start_text.SetActive(true);
                flashtime = false;
            }
            else
            {
                Start_text.SetActive(false);
                flashtime = true;
            }
            time = 0.0f;
        }
    }

    //クレジットメニューを出すときに呼び出す
    public void OpenClegit()
    {
        ClegitIcon.SetActive(false);
        TitleText.SetActive(false);
        icon = 1;
        Start_text.SetActive(false);
        ClegitMenu.SetActive(true);
    }

    //クレジットメニューを閉じるときに呼び出す
    public void CloseClegit()
    {
        ClegitMenu.SetActive(false);
        TitleText.SetActive(true);
        icon = 0;
        Start_text.SetActive(true);
        ClegitIcon.SetActive(true);
    }

    //メインメニューへ移行
    public void GoMain()
    {
        SceneManager.LoadScene(1);   
    }
}
