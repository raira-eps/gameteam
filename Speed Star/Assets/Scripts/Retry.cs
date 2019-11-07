using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    ///<summary>
    ///1　スコアの初期化
    ///2　速度の初期化
    ///3　コメント文を書く
    /// </summary>
    public void ButtomRetry()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        //SceneManager.GetActiveScene().name で現在のシーンの名前を取得
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode) => Time.timeScale = 1f;
}
