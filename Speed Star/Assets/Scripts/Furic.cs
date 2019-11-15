using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class Furic : MonoBehaviour
{
    Vector2 touchStartPos;                                                            //タッチの始点取得変数
    List<Vector2> touchMovePos = new List<Vector2>();               //タッチの中間点取得変数
    Vector2 touchEndPos;                                                             //タッチの終点取得変数
    string Direction;                                                                     //フリックの方向取得変数
    bool c;

    void Update()
    {
#if UNITY_EDITOR
        #region PC用タッチ処理
        //タッチの始点を取得
        if (Input.GetMouseButtonDown(0)) {
            touchStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        //タッチの中点を取得
        if (Input.GetMouseButton(0)) {
            touchMovePos.Add(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            GetMoveDirection(Input.mousePosition.x - touchStartPos.x, Input.mousePosition.y - touchStartPos.y, 50, 75);
        }

        //タッチの終点を取得
        if (Input.GetMouseButtonUp(0)) {
            touchEndPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            GetDirection();
        }
        #endregion
#else
        #region iPhone用タッチ処理
        //タッチの始点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStartPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }

        //タッチの中点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchMovePos.add(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
           GetMoveDirection(Input.mousePosition.x - touchStartPos.x, Input.mousePosition.y - touchStartPos.y, 50, 75);
        }

        //タッチの終点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchEndPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            GetDirection();
        }
        #endregion
#endif
    }

    void GetMoveDirection(float x, float y, float minR, float maxR)
    {
        float r = 0;
        r = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));

        if (minR <= r && r <= maxR)
            Debug.Log("OK!");


        //float directionX = touchStartPos.x;
        //float directionY = touchStartPos.y;
        //if (30 < directionX && 30 < directionY) Direction = "rightUp";
        //else if (30 < directionX && -30 < directionY) Direction = "rightDown";
        //else if (-30 > directionX && 30 > directionY) Direction = "leftUp";
        //else if (-30 > directionX && - 30 > directionY) Direction = "leftDown";
    }

    //フリックの方向を取得
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX)) {
            if (30 < directionX) Direction = "right";  //右向きにフリック
            else if (-30 > directionX) Direction = "left";  //左向きにフリック
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY)) {
            if (30 < directionY) Direction = "up";  //上向きにフリック
            else if (-30 > directionY) Direction = "down";  //下向きのフリック
        }
        else Direction = "touch";  //タッチを検出
    }

    void DirectionCheck()
    {
        switch (Direction) {
            case "right":
                Debug.Log("right");
                break;

            case "left":
                Debug.Log("left");
                break;

            case "up":
                Debug.Log("up");
                break;

            case "down":
                Debug.Log("down");
                break;

            default:
                break;
        }
    }
}
