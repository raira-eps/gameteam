using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class Furic : MonoBehaviour
{
    Vector2 touchStartPos;                                                            //タッチの始点取得変数
    Vector2 circlePos;
    List<Vector2> touchMovePos = new List<Vector2>();               //タッチの中間点取得変数
    string direction;                                                                     //フリックの方向取得変数
    List<string> circleDirection = new List<string>();                   //円フリックの方向取得変数
    bool isCircleCheck;
    float dir;
    int i;

    void Update()
    {
#if UNITY_EDITOR
        #region PC用タッチ処理
        //タッチの始点を取得
        if (Input.GetMouseButtonDown(0)) {
            touchStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            isCircleCheck = true; i = 0;
        }

        //タッチの中点を取得
        if (Input.GetMouseButton(0)) {
            Vector2 vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            touchMovePos.Add(vector);
            if (dir < Vector2.Distance(touchStartPos, vector)) {
                dir = Vector2.Distance(touchStartPos, vector);
                circlePos = vector;
            }
            i++;
        }

        //タッチの終点を取得
        if (Input.GetMouseButtonUp(0)) {
            Vector2 touchEndPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            GetDirection(touchStartPos.x + (circlePos.x - touchStartPos.x)/2, touchStartPos.y + (circlePos.y - touchStartPos.y)/2, (dir - 500)/2, (dir + 500)/2);
        }
        #endregion
#else
        #region iPhone用タッチ処理
        //タッチの始点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            touchStartPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }

        //タッチの中点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Moved) {
           touchMovePos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
           GetMoveDirection(touchMovePos.x - touchStartPos.x, touchMovePos.y - touchStartPos.y, 50, 75);
        }

        //タッチの終点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            touchEndPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            GetDirection();
        }
        #endregion
#endif
    }

    void GetMoveDirection(float x, float y, float minR, float maxR)
    {
        float r = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));     //ピタゴラスの定理（X*X + Y*Y = R*R）

        if (minR <= r && r <= maxR) Debug.Log("OK!");

        if (x > 0 && y > 0) circleDirection.Add("UpRightCircle");  //もしx > 0, y > 0なら上半右回転
        if (x < 0 && y < 0) circleDirection.Add("DownRightCircle");  //もしx < 0, y < 0なら下半右回転
        if (x < 0 && y > 0) circleDirection.Add("UpLeftCircle");  //もしx < 0, y > 0なら上半左回転
        if (x > 0 && y < 0) circleDirection.Add("DownLeftCircle");  //もしx > 0, y < 0なら下半左回転
    }

    //フリックの方向を取得
    void GetDirection(float directionX, float directionY, float minR, float maxR)
    {
        foreach (var pos in touchMovePos) {
            //ピタゴラスの定理 X*X + Y*Y = R*R => R = (X*X + Y*Y) / R
            float r = Mathf.Sqrt(Mathf.Pow(pos.x - directionX, 2) + Mathf.Pow(pos.y - directionY , 2));
            if (minR <= r && r <= maxR) {
                if (pos.x > directionX && pos.y > directionY) circleDirection.Add("UpRightCircle");  //もしx > 0, y > 0なら上半右回転
                if (pos.x < directionX && pos.y < directionY) circleDirection.Add("DownRightCircle");  //もしx < 0, y < 0なら下半右回転
                if (pos.x < directionX && pos.y > directionY) circleDirection.Add("UpLeftCircle");  //もしx < 0, y > 0なら上半左回転
                if (pos.x > directionX && pos.y < directionY) circleDirection.Add("DownLeftCircle");  //もしx > 0, y < 0なら下半左回転
            }
            else
            {
                isCircleCheck = false; 
                Debug.Log(r + " : " + minR + "/" + maxR);
            }
        }

        if (!isCircleCheck) {
            if (Mathf.Abs(directionY) < Mathf.Abs(directionX)) {
                if (30 < directionX) direction = "right";  //右向きにフリック
                else if (-30 > directionX) direction = "left";  //左向きにフリック
            }
            else if (Mathf.Abs(directionX) < Mathf.Abs(directionY)) {
                if (30 < directionY) direction = "up";  //上向きにフリック
                else if (-30 > directionY) direction = "down";  //下向きのフリック
            }
            else direction = "touch";  //タッチを検出
        }

        DirectionCheck();
    }

    void DirectionCheck()
    {
        if(isCircleCheck)
            switch (circleDirection[0]) {
                case "UpRightCircle":
                    Debug.Log("UpRightCircle");
                    break;

                case "DownRightCircle":
                    Debug.Log("DownRightCircle");
                    break;

                case "UpLeftCircle":
                    Debug.Log("UpLeftCircle");
                    break;

                case "DownLeftCircle":
                    Debug.Log("DownLeftCircle");
                    break;

                default:
                    break;
            }

        else
            switch (direction) {
                case "right":
                    Debug.Log("right");
                    direction = null;
                    break;

                case "left":
                    Debug.Log("left");
                    direction = null;
                    break;

                case "up":
                    Debug.Log("up");
                    direction = null;
                    break;

                case "down":
                    Debug.Log("down");
                    direction = null;
                    break;

                default:
                    break;
            }
    }
}
