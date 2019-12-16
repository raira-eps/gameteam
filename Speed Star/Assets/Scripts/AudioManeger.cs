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
        Landing,             //着地SE
        GoalSE               //歓声SE
    }
    public enum BGM
    {
        Asahi,
        Moruga
    }
    public enum Voice
    {
        Jump,              //ジャンプボイス
        AreaJump,          //エリアジャンプボイス
        TrickJump,         //トリックボイス
        Excellent,         //トリック,エクセレント評価ボイス
        Good,              //トリック,グッド評価ボイス
        Normal,            //トリック,ノーマル評価ボイス
        Miss,              //トリック,ミス評価ボイス
        Damage,            //ダメージボイス
        Goal,              //ゴール時ボイス
    }
    static AudioSource[] Audio;
    //SE
    [SerializeField] static AudioClip countdown;          //エアフェンスカウントダウンSE
    [SerializeField] static AudioClip sucusses;           //エアフェンス成功SE
    [SerializeField] static AudioClip but;                //エアフェンス失敗SE
    [SerializeField] static AudioClip boost;              //ブーストSE
    [SerializeField] static AudioClip _short;　　　　　   //ショートSE
    [SerializeField] static AudioClip banana;             //バナナSE
    [SerializeField] static AudioClip tipSE;              //チップ獲得SE
    [SerializeField] static AudioClip GameOverSE;　　　　 //ゲームオーバーSE
    [SerializeField] static AudioClip landing;            //着地SE
    [SerializeField] static AudioClip goal;

    //BGM
    [SerializeField] static AudioClip AsahiBGM;
    [SerializeField] static AudioClip MorugaBGM;

    //モルガ声
    [SerializeField] static AudioClip MorugaJumpVoise;
    [SerializeField] static AudioClip MorugaAreaJumpVoice;
    [SerializeField] static AudioClip MorugaTrickJumpVoice;
    [SerializeField] static AudioClip MorugaExcellentVoice;
    [SerializeField] static AudioClip MorugaGoodVoice;
    [SerializeField] static AudioClip MorugaNormalVoice;
    [SerializeField] static AudioClip MorugaMissVoice;
    [SerializeField] static AudioClip MorugaDamageVoice;
    [SerializeField] static AudioClip MorugaGaolVoice;
    [SerializeField] static AudioClip MorugaLanding;

    //旭声
    [SerializeField] static AudioClip AsahiJumpVoice;
    [SerializeField] static AudioClip AsahiAreaJumpVoice;
    [SerializeField] static AudioClip AsahiTrickJumpVoice;
    [SerializeField] static AudioClip AsahiExcellentVoice;
    [SerializeField] static AudioClip AsahiGoodVoice;
    [SerializeField] static AudioClip AsahiNormalVoice;
    [SerializeField] static AudioClip AsahiMissVoice;
    [SerializeField] static AudioClip AsahiDamagiVoice;
    [SerializeField] static AudioClip AsahiGoalVoice;
    [SerializeField] static AudioClip AsahiLanding;
    void Start()
    {
        //----------------------SE-----------------------------------------------------
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
        goal = Resources.Load<AudioClip>("Sounds/SE/speedster_歓声SE");

        //----------------------BGM-----------------------------------------------------
        AsahiBGM = Resources.Load<AudioClip>("Sounds/BGM/AsahiPlayBGM");
        MorugaBGM = Resources.Load<AudioClip>("Sounds/BGM/speedster_Morga is IDOL☆");

        //----------------------Voice-----------------------------------------------------
        //モルガ
        MorugaJumpVoise = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaAreaJumpVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaTrickJumpVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaExcellentVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaGoodVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaNormalVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaMissVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaDamageVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/");
        MorugaGaolVoice = Resources.Load<AudioClip>("Sounds/Voice/Moruga/モルガ　ラン　ゴール");
        MorugaLanding = Resources.Load<AudioClip>("Sounds/Voice/Moruga/モルガ　ジャンプ　3");
        //旭
        AsahiJumpVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiAreaJumpVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiTrickJumpVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiExcellentVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiGoodVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiNormalVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiMissVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiDamagiVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/");
        AsahiGoalVoice = Resources.Load<AudioClip>("Sounds/Voice/Asahi/speedsrter_旭ボイス　ラン１0");
        AsahiLanding = Resources.Load<AudioClip>("Sounds/Voice/Asahi/speedster_旭ボイス　ラン　３");
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
                if (PlayerPrefs.GetInt("chara") == 1)
                {
                    Audio[0].PlayOneShot(MorugaLanding);
                    Debug.Log("モルガ足音");
                }
                else if (PlayerPrefs.GetInt("chara") == 2)
                {
                    Audio[0].PlayOneShot(AsahiLanding);
                    Debug.Log("旭足音");
                }
                break;
            case SE.GoalSE:
                Audio[0].PlayOneShot(goal);
                Debug.Log("歓声");
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
    static public void VoiceSE(Voice sound)
    {
        if (PlayerPrefs.GetInt("chara") == 1)
        {
            //モルガ声　処理
            switch (sound)
            {
                case Voice.Jump:
                    // Audio[0].PlayOneShot(MorugaJumpVoise);
                    Debug.Log("モルガジャンプ");
                    break;
                case Voice.AreaJump:
                    // Audio[0].PlayOneShot(MorugaAreaJumpVoice);
                    Debug.Log("モルガエリアジャンプ");
                    break;
                case Voice.TrickJump:
                    //  Audio[0].PlayOneShot(MorugaTrickJumpVoice);
                    Debug.Log("モルガトリック");
                    break;
                case Voice.Excellent:
                    //   Audio[0].PlayOneShot(MorugaExcellentVoice);
                    Debug.Log("モルガトリック判定エクセレント");
                    break;
                case Voice.Good:
                    //  Audio[0].PlayOneShot(MorugaGoodVoice);
                    Debug.Log("モルガトリック判定グッド");
                    break;
                case Voice.Normal:
                    //  Audio[0].PlayOneShot(MorugaNormalVoice);
                    Debug.Log("モルガトリック判定ノーマル");
                    break;
                case Voice.Miss:
                    // Audio[0].PlayOneShot(MorugaMissVoice);
                    Debug.Log("モルガトリック判定ミス");
                    break;
                case Voice.Damage:
                    //  Audio[0].PlayOneShot(MorugaDamageVoice);
                    Debug.Log("モルガバナナダメージ");
                    break;
                case Voice.Goal:
                    //  Audio[0].PlayOneShot(MorugaGaolVoice);
                    Debug.Log("モルガゴールボイス");
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefs.GetInt("chara") == 2)
        {
            //旭声　処理
            switch (sound)
            {
                case Voice.Jump:
                    //  Audio[0].PlayOneShot(AsahiJumpVoice);
                    Debug.Log("旭ジャンプ");
                    break;
                case Voice.AreaJump:
                    //  Audio[0].PlayOneShot(AsahiAreaJumpVoice);
                    Debug.Log("旭エリアジャンプ");
                    break;
                case Voice.TrickJump:
                    //   Audio[0].PlayOneShot(AsahiTrickJumpVoice);
                    Debug.Log("旭トリックジャンプ");
                    break;
                case Voice.Excellent:
                    //   Audio[0].PlayOneShot(AsahiExcellentVoice);
                    Debug.Log("旭トリック判定エクセレント");
                    break;
                case Voice.Good:
                    //  Audio[0].PlayOneShot(AsahiGoodVoice);
                    Debug.Log("旭トリック判定グッド");
                    break;
                case Voice.Normal:
                    //   Audio[0].PlayOneShot(AsahiNormalVoice);
                    Debug.Log("旭トリック判定ノーマル");
                    break;
                case Voice.Miss:
                    //   Audio[0].PlayOneShot(AsahiMissVoice);
                    Debug.Log("旭トリック判定ミス");
                    break;
                case Voice.Damage:
                    //  Audio[0].PlayOneShot(AsahiDamagiVoice);
                    Debug.Log("旭バナナダメージ");
                    break;
                case Voice.Goal:
                    Audio[0].PlayOneShot(AsahiGoalVoice);
                    Debug.Log("旭ゴールボイス");
                    break;
                default:
                    break;
            }
        }
    }
}
