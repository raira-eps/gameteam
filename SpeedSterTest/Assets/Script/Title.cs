using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] AudioSource TitleBGM;
    [SerializeField] GameObject Start_text;
    [SerializeField] GameObject ClegitMenu;
    [SerializeField] GameObject ClegitIcon;
    [SerializeField] AudioClip Sound1;
    [SerializeField] AudioClip Sound2;

    private float flash = 2.0f;
    private float time = 0.0f;
    private bool flashtime = true;
    void Start()
    {
        //TitleBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= flash)
        {
            if(flashtime == true)
            {
                Start_text.SetActive(false);
                flashtime = false;
            }
            else
            {
                Start_text.SetActive(true);
                flashtime = true;
            }

            time = 0.0f;
        }
    }

    public void OpenClegit()
    {
        ClegitIcon.SetActive(false);
        ClegitMenu.SetActive(true);
    }

    public void CloseClegit()
    {
        ClegitMenu.SetActive(false);
        ClegitIcon.SetActive(true);
    }
}
