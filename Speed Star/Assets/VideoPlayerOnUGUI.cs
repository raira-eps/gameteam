using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage), typeof(VideoPlayer), typeof(AudioSource))]
public class VideoPlayerOnUGUI : MonoBehaviour
{
    [SerializeField] GameObject saisei,ripito,right,left;
    RawImage image;
    VideoPlayer Videoplayer;
    double length;
    bool finish = false ,re = false;
    void Awake()
    {
        image = GetComponent<RawImage>();
        Videoplayer = GetComponent<VideoPlayer>();
        var source = GetComponent<AudioSource>();
        Videoplayer.EnableAudioTrack(0, true);
        Videoplayer.SetTargetAudioSource(0, source);
        length = Videoplayer.clip.length;
    }
    void Update()
    {
        if (Videoplayer.isPrepared)
        {
            image.texture = Videoplayer.texture;
        }

        if (Videoplayer.time >= length - 1)
        {
            Videoplayer.Stop();
            saisei.SetActive(false);
            ripito.SetActive(true);
            right.SetActive(true);
            left.SetActive(true);
            re = true;
        }
    }

    public void Stop()
    {
        if (re == true)
        {
            Videoplayer.time = 0.0f;
            Videoplayer.Play();
            ripito.SetActive(false);
            re = false;
        }
        //　再生中でなければ再生
        else if (!Videoplayer.isPlaying)
        {
            right.SetActive(false);
            left.SetActive(false);
            saisei.SetActive(false);
            Videoplayer.Play();
            //　再生中であれば停止
        }
        else
        {
            right.SetActive(true);
            left.SetActive(true);
            saisei.SetActive(true);
            Videoplayer.Pause();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Videoplayer.time = length-1;
        }
    }
}