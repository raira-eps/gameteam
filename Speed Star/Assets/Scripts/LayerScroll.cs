using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerScroll : MonoBehaviour
{
    [SerializeField] Transform target;
    [Header("シーン実行中は値調整出来ません")]

    /// <summary>
    /// 画面スクロールの速さ
    /// </summary>
    [SerializeField, Range(1, 20)] float scrollSpeed;

    void Start()
    {
        //インスペクターで設定したスクロールスピードを処理で扱いやすい値にキャスト
        scrollSpeed = 10 / scrollSpeed;
        scrollSpeed *= 200;
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, 90);
        //ここのスクロールスピードにはStartで調整された値が入る
        Vector2 offset = new Vector2(transform.position.x / scrollSpeed, 0);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
