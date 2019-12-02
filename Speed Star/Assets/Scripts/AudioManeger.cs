using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    public enum SE
    {
        TrikcCountdownSE,
        SucusseSE,
        ButSE,
        BananaSE
    }
    static AudioSource[] Audio;

    [SerializeField] static AudioClip countdown;
    [SerializeField] static AudioClip sucusses;
    [SerializeField] static AudioClip but;

    void Start()
    {
        Audio = GetComponents<AudioSource>();
        countdown = Resources.Load<AudioClip>("Sounds/SE/Countdown");
        sucusses = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック成功1");
        but = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック失敗");
    }

    void Update()
    {

    }

    static public void SoundSE(SE sound)
    {
        switch (sound)
        {
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
            case SE.BananaSE:
                Debug.Log("バナナ音");
                break;
            default:
                break;
        }
    }
}
