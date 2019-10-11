using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InOption()
    {
        OptionMenu.SetActive(true);
    }

    public void OffOption()
    {
        OptionMenu.SetActive(false);
    }

    public void GoStageKettei()
    {
        SceneManager.LoadScene(2);
    }

    public void GoCharaKettei()
    {
        SceneManager.LoadScene(3);
    }
}
