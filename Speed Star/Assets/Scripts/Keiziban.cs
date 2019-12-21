using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keiziban : MonoBehaviour
{
    [SerializeField] GameObject Denki1;
    [SerializeField] GameObject Denki2;
    [SerializeField] GameObject Denki3;
    GameObject[] eki = new GameObject[4];

    private void Awake()
    {
        eki[1] = Denki1;
        eki[2] = Denki2;
        eki[3] = Denki3;
        StartCoroutine("RandomDenki");
    }

    //電光掲示板を乱打ミニダス
    IEnumerator RandomDenki()
    {
        while (true)
        {
            int index = Random.Range(1, 4);
            Debug.Log(index);
            eki[index].SetActive(true);
            yield return new WaitForSeconds(15.0f);
            eki[index].SetActive(false);
        }
    }
}
