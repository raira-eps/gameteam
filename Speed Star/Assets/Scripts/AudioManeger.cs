using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AudioManeger : MonoBehaviour
{
    public enum SE  
    {
        TrikcCountdownSE,
        SucusseSE, 
        ButSE
    }
    AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public void SoundSE(SE sound)
    {
        switch (sound) {
            case SE.TrikcCountdownSE:
               var _countdown =  Resources.Load<AudioClip>("Sounds/SE/speedster_トリックスポットカウントダウン");
                Audio.PlayOneShot(_countdown);
                break;
            case SE.SucusseSE:
                var _sucusses = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック成功1");
                break;
            case SE.ButSE:
                var _but = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック失敗");
                break;
            default:
                break;
        }
    }
}
