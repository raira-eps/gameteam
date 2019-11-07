using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float countTime = 0;
    int minutes = 0;
    TextMeshProUGUI minuteText;
    TextMeshProUGUI secondsText;

    // Start is called before the first frame update
    void Start()
    {
        minuteText = GameObject.Find("MinuteText").GetComponent<TextMeshProUGUI>();
        secondsText = GameObject.Find("SecondsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        countTime += Time.deltaTime;
        minuteText.text = minutes.ToString() + ":   ";
        secondsText.text = countTime.ToString("F2");

        if (countTime >= 60.0f) {
            minutes += 1;
            countTime = 0;
        }
    }
}
