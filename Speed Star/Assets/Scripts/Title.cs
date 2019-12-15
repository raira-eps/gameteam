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

    [SerializeField] AudioSource SE1, SE2, BGM;
    private new AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine("SEE");
        //ここでステージ１とステージ２の最高スコアを初期化しているが本来はこんな風に書きません！！（テスト用です）
        PlayerPrefs.SetInt("Shibuya", 0);
        PlayerPrefs.SetInt("Akihabara", 0);
        PlayerPrefs.SetInt("chara", 1);
    }

    //起動した際にＳＥとＢＧＭを流す
    IEnumerator SEE()
    {
        yield return new WaitForSeconds(1f);
        int i = Random.Range(1, 3);
        if (i == 1)
        {
            SE1.Play();
        }
        else
        {
            SE2.Play();
        }
        yield return new WaitForSeconds(1f);
        BGM.Play();
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
