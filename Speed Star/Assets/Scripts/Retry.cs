using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    ///<summary>
    ///1　スコアの初期化
    ///2　速度の初期化
    ///3　コメント文を書く
    /// </summary>
    public void ButtomRetry()
    {
        //Seane No4 は渋谷
        SceneManager.LoadScene(4);
    }
}
