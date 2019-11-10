using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject chara1;
    //イニシャルY_Y

    [SerializeField]
    GameObject MRG;
    [SerializeField]
    GameObject ASH;
    [SerializeField]
    GameObject Moru_panel;
    [SerializeField]
    GameObject Asa_panel;

    //後ろの色を変える処理
    public void Start()
    {
        if (PlayerPrefs.GetInt("chara") == 2)
        {
            MRG.SetActive(false);
            ASH.SetActive(true);
        }
        else
        {
            ASH.SetActive(false);
            MRG.SetActive(true);
        }
    }
    //メインメニューへ移行する
    public void GoMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    //ステージセレクト画面へ移行する
    public void GoStageSelect()
    {
        SceneManager.LoadScene(2);
    }

    public void Chara1()
    {
        MRG.SetActive(false);
        ASH.SetActive(true);
        PlayerPrefs.SetInt("chara", 2);
    }

    public void Chara2()
    {
        ASH.SetActive(false);
        MRG.SetActive(true);
        PlayerPrefs.SetInt("chara", 1);
    }

    //キャラクター拡大アイコンが押されたなら
    public void Kakudai()
    {
        if(PlayerPrefs.GetInt("chara") == 1)
        {
            Moru_panel.SetActive(true);
        }
        else
        {
            Asa_panel.SetActive(true);
        }
    }

    //拡大解除
    public void Non_Kakudai()
    {
        Moru_panel.SetActive(false);
        Asa_panel.SetActive(false);
    }
}
