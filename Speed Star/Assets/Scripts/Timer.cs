using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float countTime = 0;
    int minutes = 0;
    TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countTime += Time.deltaTime;
        timeText.text = minutes.ToString() + ":" + countTime.ToString("0.<size=20>00</size>");
        //secondsText.text = countTime.ToString("F2");

        if (countTime >= 60.0f) {
            minutes += 1;
            countTime = 0;
        }
    }
}
