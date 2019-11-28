using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    public enum SE  
    {
        TrikcCountdownSE,
        SucusseSE, 
        ButSE
    }
    static AudioSource[] Audio;

    [SerializeField] static AudioClip countdown;
    [SerializeField] AudioClip sucusses;
    [SerializeField] AudioClip but;

    void Start()
    {
        Audio = GetComponents<AudioSource>();
        countdown = Resources.Load<AudioClip>("Sounds/SE/Countdown");
    }

    void Update()
    {
        //Debug.Log(countdown);
    }

    static public void S()
    {
        Audio[0].PlayOneShot(countdown);
    }

    public void SoundSE(SE sound)
    {
        Debug.Log(countdown);
        switch (sound) {
            case SE.TrikcCountdownSE:
                Debug.Log(countdown);
                Audio[0].PlayOneShot(countdown);
                break;
            case SE.SucusseSE:
                Audio[0].PlayOneShot(sucusses);
                break;
            case SE.ButSE:
                Audio[0].PlayOneShot(but);
                break;
            default:
                break;
        }
    }
}
