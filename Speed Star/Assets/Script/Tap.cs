using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] ParticleSystem TapEf;
    [SerializeField] ParticleSystem SlideEf;
    [SerializeField] Camera _camera;

    void Update()
    {
        /////////////スマホタップ用
        //if (0 < Input.touchCount) {
        //    if (Input.GetTouch(0).phase == TouchPhase.Began) {
        //        var pos = _camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1) + _camera.transform.forward * 10);
        //        TapEf.transform.position = pos;
        //        TapEf.Emit(1);
        //    }
        //    else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
        //        var pos = _camera.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1) + _camera.transform.forward * 10);
        //        SlideEf.transform.position = pos;
        //        SlideEf.Emit(1);
        //    }
        //}

        /////////////マウスクリック用
        if (Input.GetMouseButtonDown(0))
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            TapEf.transform.position = pos;
            TapEf.Emit(1);
        }
        else if (Input.GetMouseButton(0))
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            SlideEf.transform.position = pos;
            SlideEf.Emit(1);
        }
    }
}
