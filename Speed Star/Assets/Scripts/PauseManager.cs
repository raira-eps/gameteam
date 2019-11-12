using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;           //ポーズした時に表示するUIのプレハブ

    //ポーズ画面を出す
    public void PauseOpen()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        //SceneManager.GetActiveScene().name で現在のシーンの名前を取得
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Retire()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        SceneManager.LoadScene(2);
    }

    //ポーズ画面を消す
    public void PauseClose()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode) => Time.timeScale = 1f;
}
