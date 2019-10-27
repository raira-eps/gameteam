using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    //イニシャルY_Y

    //シブヤオブジェクトを変更する際に使用する
    [SerializeField] GameObject StageName1_New;
    [SerializeField] GameObject StageName1_Clear;

    //アキハバラオブジェクトを変更する際に使用する
    [SerializeField] GameObject StageName2_Unlock;
    [SerializeField] GameObject StageName2_New;
    [SerializeField] GameObject StageName2_Clear;


    [SerializeField] GameObject Stage;
    [SerializeField] GameObject Kettei;
    [SerializeField] GameObject Yes_Yes;
    [SerializeField] GameObject Yes_No;
    [SerializeField] GameObject No_Yes;
    [SerializeField] GameObject No_No;
    [SerializeField] string StageName1, StageName2;
    static public　string StageName; //これから遊ぶステージの名前（リザルトシーンで使う）
    private bool Yes = true;


    public void Start()
    {
        //今はステージ１の最高スコアが1500以上ならそれをクリアしてステージ２を開放するようになっている
        if (PlayerPrefs.GetInt($"{StageName1}") >= 1500)
        {
            StageName1_New.SetActive(false);
            StageName1_Clear.SetActive(true);
            StageName2_Unlock.SetActive(false);
            StageName2_New.SetActive(true);
        }

        //今はステージ２の最高スコアが1500以上ならそれがクリアになる
        if (PlayerPrefs.GetInt("Akihabara") >= 1500)
        {
            StageName2_New.SetActive(false);
            StageName2_Clear.SetActive(true);
        }
    }


    //ステージ１が選択され場合の処理
    public void GoStageName1()
    {
        Select($"{StageName1}");
    }

    //ステージ２が選択された場合の処理
    public void GostageName2()
    {
        Select($"{StageName2}");
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

    //メニュー画面に戻る
    public void GoMenue()
    {
        SceneManager.LoadScene(1);
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
