using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //イニシャルY_Y
    [SerializeField] GameObject back_MRG;
    [SerializeField] GameObject back_ASH;
    //オプション・ヘルプ画面を入れる
    [SerializeField] GameObject OptionMenu;
    //ヘルプ画面一覧
    [SerializeField] GameObject Help_1;
    [SerializeField] GameObject Help_2;
    [SerializeField] GameObject Help_3;
    [SerializeField] GameObject Help_4;
    [SerializeField] GameObject Help_5;
    [SerializeField] GameObject Help_6;
    [SerializeField] GameObject Help_7;
    [SerializeField] GameObject Help_8;
    [SerializeField] GameObject Help_9;
    [SerializeField] GameObject oriba;

    [SerializeField] GameObject Chara1_SE1;
    [SerializeField] GameObject Chara1_SE2;
    [SerializeField] GameObject Chara1_SE3;
    [SerializeField] GameObject Chara2_SE1;
    [SerializeField] GameObject Chara2_SE2;
    [SerializeField] GameObject Chara2_SE3;

    [SerializeField] AudioClip sound01;
    [SerializeField] AudioSource audioSource, Bgm;
    GameObject[] views = new GameObject[9];
    //今選択中のヘルプ画面番号
    private int help_int = 0,chara1 = 0,chara2 = 0;

    //後ろの色を変える処理
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        views[0] = Help_1;
        views[1] = Help_2;
        views[2] = Help_3;
        views[3] = Help_4;
        views[4] = Help_5;
        views[5] = Help_6;
        views[6] = Help_7;
        views[7] = Help_8;
        views[8] = Help_9;

        if (PlayerPrefs.GetInt("chara") == 2)
        {
            back_MRG.SetActive(false);
            back_ASH.SetActive(true);
        }
        else
        {
            back_ASH.SetActive(false);
            back_MRG.SetActive(true);
        }
    }
    //オプション・ヘルプ画面を呼び出す
    public void InOption()
    {
        AudioPlay();
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
            dai = 8;
        }
        if (dai >= 9)
        {
            dai = 0;
        }
        help_int = dai;
        views[dai].SetActive(true);
    }

    //オプション・ヘルプ画面を閉じる
    public void OffOption()
    {
        AudioPlay();
        OptionMenu.SetActive(false);
    }

    //ステージセレクト画面へ移行する
    public void GoStageKettei()
    {
        AudioPlay();
        SceneManager.LoadScene(2);
    }

    //キャラクターセレクト画面へ移行する
    public void GoCharaKettei()
    {
        AudioPlay();
        SceneManager.LoadScene(3);
    }

    public void AudioPlay()
    {
        audioSource.PlayOneShot(sound01);
    }

    public void Chara1_SE()
    {
        Bgm.volume = 0.3f;
        StartCoroutine("Chara1_SEE");     
    }

    public void Chara2()
    {
        Bgm.volume = 0.3f;
        StartCoroutine("Chara2_SEE");
    }

    IEnumerator Chara1_SEE()
    {
        switch (chara1)
        {
            case 0:
                Chara1_SE1.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara1_SE1.SetActive(false);
                chara1 = 1;
                break;
            case 1:
                Chara1_SE2.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara1_SE2.SetActive(false);
                chara1 = 2;
                break;
            case 2:
                Chara1_SE3.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara1_SE3.SetActive(false);
                chara1 = 0;
                break;
        }
        Bgm.volume = 0.3f;
    }
    IEnumerator Chara2_SEE()
    {
        switch (chara2)
        {
            case 0:
                Chara2_SE1.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara2_SE1.SetActive(false);
                chara2 = 1;
                break;
            case 1:
                Chara2_SE2.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara2_SE2.SetActive(false);
                chara2 = 2;
                break;
            case 2:
                Chara2_SE3.SetActive(true);
                yield return new WaitForSeconds(5f);
                Chara2_SE3.SetActive(false);
                chara2 = 0;
                break;
        }
        Bgm.volume = 0.3f;
    }
}
