using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject chara1;
    //イニシャルY_Y

    //後ろの色を変える処理
    public void Start()
    {
        Image image = chara1.GetComponent<Image>();
        switch (PlayerPrefs.GetInt("chara"))
        {
            case 0:
                break;
            case 1:
                image.color = new Color(1, 0, 0, 0.5f);
                break;
            case 2:
                image.color = new Color(0, 0, 1, 0.5f);
                break;
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
        Image image = chara1.GetComponent<Image>();
        image.color = new Color(1f, 0f, 0f, 0.5f);
        PlayerPrefs.SetInt("chara", 1);
    }

    public void Chara2()
    {
        Image image = chara1.GetComponent<Image>();
        image.color = new Color(0f, 0f, 1f, 0.5f);
        PlayerPrefs.SetInt("chara", 2);
    }
}
