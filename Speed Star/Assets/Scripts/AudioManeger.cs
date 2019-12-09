using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    //制作　山藤優真!
    public enum SE
    {
        TrikcCountdownSE,    //エアフェンスカウントダウンSE
        SucusseSE,           //エアフェンス成功SE
        ButSE,               //エアフェンス失敗SE
        BananaSE,            //バナナSE
        BoostSE,             //ブーストSE
        ShortSE,             //ショートSE
        TipsSE,              //チップ獲得SE
        GameOverSE,          //ゲームオーバーSE
        Landing,              //着地SE
    }
    public enum BGM
    {
        Asahi,
        Moruga
    }
    static AudioSource[] Audio;

    [SerializeField] static AudioClip countdown;          //エアフェンスカウントダウンSE
    [SerializeField] static AudioClip sucusses;           //エアフェンス成功SE
    [SerializeField] static AudioClip but;                //エアフェンス失敗SE
    [SerializeField] static AudioClip boost;              //ブーストSE
    [SerializeField] static AudioClip _short;　　　　　   //ショートSE
    [SerializeField] static AudioClip banana;             //バナナSE
    [SerializeField] static AudioClip tipSE;              //チップ獲得SE
    [SerializeField] static AudioClip GameOverSE;　　　　 //ゲームオーバーSE
    [SerializeField] static AudioClip landing;            //着地SE

    [SerializeField] static AudioClip AsahiBGM;
    [SerializeField] static AudioClip MorugaBGM;

    void Start()
    {
        Audio = GetComponents<AudioSource>();
        countdown = Resources.Load<AudioClip>("Sounds/SE/Countdown");
        sucusses = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック成功1");
        but = Resources.Load<AudioClip>("Sounds/SE/speedster_トリック失敗");
        boost = Resources.Load<AudioClip>("Sounds/SE/speedster_ブーストフェンスSE１０");
        _short = Resources.Load<AudioClip>("Sounds/SE/speedster_ショートフェンスSE５");
        banana = Resources.Load<AudioClip>("Sounds/SE/speedster_バナナSE");
        tipSE = Resources.Load<AudioClip>("Sounds/SE/speedster_チップ獲得時のSE");
        GameOverSE = Resources.Load<AudioClip>("Sounds/SE/speedster_ゲームオーバー");
        landing = Resources.Load<AudioClip>("Sounds/SE/speedster_着地SE");

        AsahiBGM = Resources.Load<AudioClip>("Sounds/BGM/AsahiPlayBGM");
        MorugaBGM = Resources.Load<AudioClip>("Sounds/BGM/speedster_Morga is IDOL☆");
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
                Audio[0].PlayOneShot(banana);
                break;
            case SE.BoostSE:
                Audio[0].PlayOneShot(boost);
                break;
            case SE.ShortSE:
                Audio[0].PlayOneShot(_short);
                break;
            case SE.TipsSE:
                Audio[0].PlayOneShot(tipSE);
                break;
            case SE.GameOverSE:
                Audio[0].PlayOneShot(GameOverSE);
                break;
            case SE.Landing:
                Audio[0].PlayOneShot(landing);
                break;
            default:
                break;
        }
    }
    static public void SoundBGM(BGM sound)
    {
        switch (sound)
        {
            case BGM.Asahi:
                Audio[0].PlayOneShot(AsahiBGM);
                break;
            case BGM.Moruga:
                Audio[0].PlayOneShot(MorugaBGM);
                break;
            default:
                break;
        }

    }
}
