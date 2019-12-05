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
        BananaSE,
        BoostSE,
        ShortSE,
    }
    static AudioSource[] Audio;

    [SerializeField] static AudioClip countdown;
    [SerializeField] static AudioClip sucusses;
    [SerializeField] static AudioClip but;
    [SerializeField] static AudioClip boost;
    [SerializeField] static AudioClip _short;

    void Start()
    {
        Audio = GetComponents<AudioSource>();
        countdown = Resources.Load<AudioClip>("Sounds/SE/Countdown");
        sucusses = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック成功1");
        but = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック失敗");
        boost = Resources.Load<AudioClip>("Sounds/SE/speedster_ブーストフェンスSE１０");
        _short = Resources.Load<AudioClip>("Sounds/SE/speedster_ショートフェンスSE５");
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
            case SE.BoostSE:
                Audio[0].PlayOneShot(boost);
                break;
            case SE.ShortSE:
                Audio[0].PlayOneShot(_short);
                break;
            default:
                break;
        }
    }
}
