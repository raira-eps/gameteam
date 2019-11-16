using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerScroll : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 startPosition;

    [SerializeField, Range(0, 10)] float scrollSpeed;
    float layerSizeX = 235f;

    void Start() => startPosition = transform.position;

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, layerSizeX);
        transform.position = startPosition + Vector3.left * newPosition + target.position;
    }
}
