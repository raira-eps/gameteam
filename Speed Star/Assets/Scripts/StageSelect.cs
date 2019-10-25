using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject Shibuya_new;
    [SerializeField] GameObject Shibuya_Clear;
    [SerializeField] GameObject Akihabara_Unlock;
    [SerializeField] GameObject Akihabara_New;
    [SerializeField] GameObject Akihabara_Crear;
    [SerializeField] GameObject Stage;
    [SerializeField] GameObject Kettei;
    [SerializeField] GameObject Yes_Yes;
    [SerializeField] GameObject Yes_No;
    [SerializeField] GameObject No_Yes;
    [SerializeField] GameObject No_No;
    static public string StageName;                    //これから遊ぶステージの名前（リザルトシーンで使う）
    private bool Yes = true;
    private bool shibuya = true , akihabara = false;
    public void Start()
    {
        if(PlayerPrefs.GetInt("Shibuya") >= 1500 && shibuya == true)
        {
            Shibuya_new.SetActive(false);
            Shibuya_Clear.SetActive(true);
            Akihabara_Unlock.SetActive(false);
            Akihabara_New.SetActive(true);
            shibuya = false;
            akihabara = true;
        }
        if (PlayerPrefs.GetInt("Akihabara") >= 1500)
        {
            Akihabara_New.SetActive(false);
            Akihabara_Crear.SetActive(true);
        }
    }
    //メニュー画面に戻る
    public void GoMenue()
    {
        SceneManager.LoadScene(1);
    }

    //ステージを選択し、プレイするか確認を行う
    public void Select(string Name)
    {
        StageName = Name;
        Stage.SetActive(false);
        Kettei.SetActive(true);
    }

    //前の項目でYesが押され場合、ゲーム画面を呼ぶ
    public void Decision()
    {
        SceneManager.LoadScene(4);
    }

    //前の画面でNoがおさればあい、ステージを選ぶ画面に戻す
    public void Cancel()
    {
        Stage.SetActive(true);
        Kettei.SetActive(false);
    }

    //Shibuyaが選択され場合の処理
    public void Shibuya()
    {
        Select("Shibuya");
    }

    //Akihabaraが選択された場合の処理
    public void Ahihabara()
    {
        if(akihabara == true)
        {
            Select("Akihabara");
        }
    }

    //Yesのボタンに重なったとき
    public void YesKasanari()
    {
        if(Yes == false)
        {
            Henka(false,true);
            Yes = true;
        }
    }

    //Noのボタンに重なったとき
    public void NoKasanari()
    {
        if (Yes == true)
        {
            Henka(true, false);
            Yes = false;
        }
    }

    public void Henka(bool yes, bool no)
    {
            No_Yes.SetActive(yes);
            No_No.SetActive(no);
            Yes_Yes.SetActive(no);
            Yes_No.SetActive(yes);
    }
}
