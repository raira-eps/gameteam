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
    int i = 0;

    void Update()
    {
        //タッチの始点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            touchStartPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        //タッチの中点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Moved) {
            touchMovePos[i] = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            i++;
        }

        //タッチの終点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            touchEndPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            GetDirection();
        }

        DirectionCheck();
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
        switch (Direction)
        {
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
