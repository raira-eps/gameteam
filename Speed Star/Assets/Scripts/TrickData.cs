using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrickData : MonoBehaviour
{
    [SerializeField] Image target1;
    [SerializeField] Image target2;
    [SerializeField] Image target3;
    [SerializeField] Image trickGauge;

    /// <summary>
    /// トリックのデータ
    /// </summary>
    static string[,] _data;

    /// <summary>
    /// 渋谷のトリックのデータ
    /// </summary>
    string[,] _data1 = new string[,] { 
        { "up", "down", "up" }, { "left", "up", "right" }, { "rightup", "down", "rightdown" },
    };

    /// <summary>
    /// 秋葉のトリックのデータ
    /// </summary>
    string[,] _data2 = new string[,] {
        { "UpRightCircle", "rightup", "leftdown" }, { "leftup", "DownLeftCircle", "rightup" }, { "rightdown", "down", "down" },
    };
    static int i = 0, j = 0, c = 0;

    void Start()
    {
            if (SceneManager.GetActiveScene().name == "Sibuya")
                _data = _data1;
            else if (SceneManager.GetActiveScene().name == "Akiba")
                _data = _data2;
    }

    void Update()
    {
        switch (j) {
            case 0:
                target1.color = Color.yellow;
                target2.color = Color.white;
                target3.color = Color.white;
                break;
            case 1:
                target1.color = Color.white;
                target2.color = Color.yellow;
                target3.color = Color.white;
                break;
            case 2:
                target1.color = Color.white;
                target2.color = Color.white;
                target3.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            trickGauge.fillAmount = 1;
            target1.enabled = true;
            target2.enabled = true;
            target3.enabled = true;
            Data();
            StartCoroutine(Gauge());
        }
    }

    void Data()
    {
        if (i == 0)
            SpriteChange(i);
        else if (i == 1)
            SpriteChange(i);
        else if(i == 2)
            SpriteChange(i);
    }

    void SpriteChange(int i)
    {
        target1.sprite = Resources.Load<Sprite>($"Trick/{_data[i, 0]}");
        target2.sprite = Resources.Load<Sprite>($"Trick/{_data[i, 1]}");
        target3.sprite = Resources.Load<Sprite>($"Trick/{_data[i, 2]}");
    }

    static public void Trick(string t)
    {
        if (_data[i, j] == t) {
            if (j < 2) {
                j++;
            }
            else {
                j = 0;
                i++;
                c = 1;
            }
        }
    }

    IEnumerator Gauge()
    {
        for (int i = 0; i <= 340; i++) {
            yield return new WaitForFixedUpdate();
            trickGauge.fillAmount -= 0.003f;Debug.Log(i);
            if (c == 1) {
                c = 0;
                target1.enabled = false;
                target2.enabled = false;
                target3.enabled = false;
                break;
            }
        }
        if (c == 1) {
            c = 0;
        }
        else {
            j = 0;
            i++;
        }
    }
}
