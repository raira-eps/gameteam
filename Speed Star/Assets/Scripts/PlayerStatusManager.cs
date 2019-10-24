using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text voltageText;
    void Start()
    {
        
    }
    void Update()
    {
        scoreText.text = "Score " + GameManager.Score;
        voltageText.text = "Voltage " + Player.voltage;
    }
}
