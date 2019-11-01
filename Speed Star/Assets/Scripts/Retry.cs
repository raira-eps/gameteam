using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    //山藤制作
   
    /// <summary>
    /// S.Yがやることリスト
    /// 1、スコアの初期化☆彡
    /// 2、リトライした後に速度を上げてもPlayerが動き出さないバグを直すこと☆彡
    /// 3、コメント文で説明を書こう☆彡
    /// </summary>
    public void ButtomRetry()
    {
        //Scene No4 は渋谷
        SceneManager.LoadScene(4);
    }
}
