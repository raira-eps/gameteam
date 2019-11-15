using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private GameObject childfence, childfence2;
    private bool IsShortfence = false;
    void Start()
    {
        Debug.Log(this.gameObject.tag);
        if (this.gameObject.tag == "ShortFence")
        {
            IsShortfence = true;
        }
    }
    void Update()
    {
        if (IsShortfence)
        {
            childfence.transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime, Space.World);
            childfence2.transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime, Space.World);
        }
        else
        {
            childfence.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
            childfence2.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.World);
        }
    }
}
