using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    //イニシャルY_Y

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
}
