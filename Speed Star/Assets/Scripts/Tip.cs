using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    // Name：aki
    void OnTriggerEnter(Collider other)
    {
        // Playerと接触した時にアイテムを非表示にする。
        if (other.tag == "Player")
        {
            //gameObject.SetActive(false);
        }
    }
}
