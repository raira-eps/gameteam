using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    public enum Sound  
    {
        TrikcCountdownSE, 
        SucusseSE, 
        ButSE
    }
    AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundSE(Sound sound)
    {
        switch (sound)
        {
            case Sound.TrikcCountdownSE:
               var _countdown =  Resources.Load("Sounds/SE/speedster_トリックスポットカウントダウン");
                Audio = (AudioSource)_countdown;
                Audio.PlayOneShot(Audio.clip);
                break;
            case Sound.SucusseSE:
               var _sucusses = Resources.Load("Sounds/SE/speedster_トリック成功1");
                break;
            case Sound.ButSE:
                var _but = Resources.Load("Sounds/SE/speedster_トリック失敗");
                break;
            default:
                break;
        }
    }
}
