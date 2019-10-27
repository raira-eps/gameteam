using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //イニシャルY_Y

    //オプション・ヘルプ画面を入れる
    [SerializeField] GameObject OptionMenu;

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
