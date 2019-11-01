using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //イニシャルY_Y
    [SerializeField] GameObject back;
    //オプション・ヘルプ画面を入れる
    [SerializeField] GameObject OptionMenu;

    //後ろの色を変える処理
    public void Start()
    {
        Image image = back.GetComponent<Image>();
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
    //オプション・ヘルプ画面を呼び出す
    public void InOption()
    {
        OptionMenu.SetActive(true);
    }

    //オプション・ヘルプ画面を閉じる
    public void OffOption()
    {
        OptionMenu.SetActive(false);
    }

    //ステージセレクト画面へ移行する
    public void GoStageKettei()
    {
        SceneManager.LoadScene(2);
    }

    //キャラクターセレクト画面へ移行する
    public void GoCharaKettei()
    {
        SceneManager.LoadScene(3);
    }
}
