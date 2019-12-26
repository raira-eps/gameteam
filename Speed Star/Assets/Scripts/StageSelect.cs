using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageSelect : MonoBehaviour
{
    //イニシャルY_Y

    //シブヤオブジェクトを変更する際に使用する
    [SerializeField] GameObject StageName1_New;
    [SerializeField] GameObject StageName1_Clear = null;
    [SerializeField] GameObject StageName1_Title;

    //アキハバラオブジェクトを変更する際に使用する
    [SerializeField] GameObject StageName2_Unlock;
    [SerializeField] GameObject StageName2_New;
    [SerializeField] GameObject StageName2_Clear;
    [SerializeField] GameObject StageName2_Title;

    [SerializeField] GameObject Stage;
    [SerializeField] GameObject Kettei;
    [SerializeField] string StageName1, StageName2;
    static public string StageName; //これから遊ぶステージの名前（リザルトシーンで使う）
    private bool Yes = true;

    [SerializeField]
    AudioClip sound01;
    AudioSource audioSource;

    private bool stage = false;
    public void Start()
    {

        audioSource = GetComponent<AudioSource>();
        //今はステージ１の最高スコアが1以上ならそれをクリアしてステージ２を開放するようになっている
        if (PlayerPrefs.GetInt($"{StageName1}") >= 1)
        {
            StageName1_New.SetActive(false);
            StageName1_Clear.SetActive(true);
            StageName2_Unlock.SetActive(false);
            StageName2_New.SetActive(true);
        }

        //今はステージ２の最高スコアが1以上ならそれがクリアになる
        if (PlayerPrefs.GetInt("Akihabara") >= 1)
        {
            StageName2_New.SetActive(false);
            StageName2_Clear.SetActive(true);
        }
    }

    //ステージ１が選択され場合の処理
    public void GoStageName1()
    {
        AudioPlay();
        StageName2_Title.SetActive(false);
        StageName1_Title.SetActive(true);
        stage = false;
        Select($"{StageName1}");
    }

    //ステージ２が選択された場合の処理
    public void GostageName2()
    {
        AudioPlay();
        StageName1_Title.SetActive(false);
        StageName2_Title.SetActive(true);
        stage = true;
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
        AudioPlay();
        if(stage == false)
        {
            SceneManager.LoadSceneAsync(4);
        }
        else
        {
            SceneManager.LoadSceneAsync(5);
        }
    }

    //前の画面でNoがおさればあい、ステージを選ぶ画面に戻す
    public void Cancel()
    {
        AudioPlay();
        Stage.SetActive(true);
        Kettei.SetActive(false);
    }

    //メニュー画面に戻る
    public void GoMenue()
    {
        AudioPlay();
        SceneManager.LoadSceneAsync (1);
    }

    public void AudioPlay()
    {
        audioSource.PlayOneShot(sound01);
    }

}
