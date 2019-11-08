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
    //ヘルプ画面一覧
    [SerializeField] GameObject Help_1;
    [SerializeField] GameObject Help_2;
    [SerializeField] GameObject Help_3;
    [SerializeField] GameObject Help_4;
    [SerializeField] GameObject Help_5;

    GameObject[] views = new GameObject[5];
    //今選択中のヘルプ画面番号
    private int help_int = 0;

    //後ろの色を変える処理
    public void Start()
    {
        views[0] = Help_1;
        views[1] = Help_2;
        views[2] = Help_3;
        views[3] = Help_4;
        views[4] = Help_5;
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

    //ヘルプ画面を切り替える（右を押されたら）
    public void Help_Kirikae_Right()
    {
        Kirikae(1);
    }

    //ヘルプ画面を切り替える（左を押されたら）
    public void Help_Kirikae_Left()
    {
        Kirikae(-1);
    }


    public void Kirikae(int i)
    {
        views[help_int].SetActive(false);
        int dai = help_int + i;
        if(dai < 0)
        {
            dai = 4;
        }
        if (dai >= 5)
        {
            dai = 0;
        }
        help_int = dai;
        views[dai].SetActive(true);
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
