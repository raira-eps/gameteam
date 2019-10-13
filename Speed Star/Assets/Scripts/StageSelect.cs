using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject Stage;
    [SerializeField] GameObject Kettei;
    public string StageName;                    //これから遊ぶステージの名前（リザルトシーンで使う）

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoMenue()
    {
        SceneManager.LoadScene(1);
    }

    public void Select(string Name)
    {
        StageName = Name;
        Stage.SetActive(false);
        Kettei.SetActive(true);
    }

    public void Decision()
    {
        SceneManager.LoadScene(4);
    }

    public void Cancel()
    {
        Stage.SetActive(true);
        Kettei.SetActive(false);
    }
}
