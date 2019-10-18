using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Playerと接触した時にアイテムを非表示にする。
        if (other.tag == "Player")
        {
            Debug.Log("Tipを獲得。");
            this.gameObject.SetActive(false);
        }
    }
}
