using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    //イニシャルY_Y
    [SerializeField] GameObject ClegitMenu;
    [SerializeField] GameObject ClegitIcon;
    [SerializeField] AudioClip Sound1;

    private new AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //ここでステージ１とステージ２の最高スコアを初期化しているが本来はこんな風に書きません！！（テスト用です）
        PlayerPrefs.SetInt("Shibuya", 0);
        PlayerPrefs.SetInt("Akihabara", 0);
        PlayerPrefs.SetInt("chara", 1);
    }

    //クレジットメニューを出すときに呼び出す
    public void OpenClegit()
    {
        MusicPlay();
        ClegitIcon.SetActive(false);
        ClegitMenu.SetActive(true);
    }

    //クレジットメニューを閉じるときに呼び出す
    public void CloseClegit()
    {
        MusicPlay();
        ClegitMenu.SetActive(false);
        ClegitIcon.SetActive(true);
    }

    public void MusicPlay()
    {
        audio.PlayOneShot(Sound1);
    }

    //メインメニューへ移行
    public void GoMain()
    {
        MusicPlay();
        SceneManager.LoadScene(1);
    }
}
