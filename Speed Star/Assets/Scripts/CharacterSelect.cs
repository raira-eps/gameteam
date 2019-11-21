﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] GameObject chara1;
    //イニシャルY_Y

    [SerializeField]
    GameObject Character_1;
    [SerializeField]
    GameObject Character_2;
    [SerializeField]
    GameObject Character1_Panel;
    [SerializeField]
    GameObject Character2_Panel;

    [SerializeField]
    AudioClip sound01;
    AudioSource audioSource;

    //後ろの色を変える処理
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //chara==1がモルガ、chara==2が旭
        if (PlayerPrefs.GetInt("chara") == 2)
        {
            Character_1.SetActive(false);
            Character_2.SetActive(true);
        }
        else
        {
            Character_2.SetActive(false);
            Character_1.SetActive(true);
        }
    }

    //メインメニューへ移行する
    public void GoMainMenu()
    {
        AudioPlay();
        SceneManager.LoadScene(1);
    }

    //ステージセレクト画面へ移行する
    public void GoStageSelect()
    {
        AudioPlay();
        SceneManager.LoadScene(2);
    }

    //キャラクター１が選択された場合の処理
    public void Chara1()
    {
        AudioPlay();
        Character_2.SetActive(false);
        Character_1.SetActive(true);
        PlayerPrefs.SetInt("chara", 1);
    }

    //キャラクター2が選択された場合の処理
    public void Chara2()
    {
        AudioPlay();
        Character_1.SetActive(false);
        Character_2.SetActive(true);
        PlayerPrefs.SetInt("chara", 2);
    }

    //キャラクター拡大アイコンが押されたなら
    public void Kakudai()
    {
        AudioPlay();
        if(PlayerPrefs.GetInt("chara") == 1)
        {
            Character1_Panel.SetActive(true);
        }
        else
        {
            Character2_Panel.SetActive(true);
        }
    }

    //拡大解除
    public void Non_Kakudai()
    {
        AudioPlay();
        Character1_Panel.SetActive(false);
        Character2_Panel.SetActive(false);
    }

    public void AudioPlay()
    {
        audioSource.PlayOneShot(sound01);
    }
}
