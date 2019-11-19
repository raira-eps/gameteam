using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//K.R
public class Furic : MonoBehaviour
{
    Vector2 touchStartPos;                                                             //タッチの始点取得変数
    Vector2 circlePos;                                                                    //円の直径
    List<Vector2> touchMovePos = new List<Vector2>();               //タッチの中間点取得変数
    string direction;                                                                      //フリックの方向取得変数
    List<string> circleDirection = new List<string>();                    //円フリックの方向取得変数
    bool isCircleCheck;
    float dir;

    void Update()
    {
#if UNITY_EDITOR
        #region PC用タッチ処理
        //タッチの始点を取得
        if (Input.GetMouseButtonDown(0)) {
            touchStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            isCircleCheck = true;
        }

        //タッチの中点を取得
        if (Input.GetMouseButton(0)) {
            Vector2 vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            touchMovePos.Add(vector);
            if (dir < Vector2.Distance(touchStartPos, vector)) {
                dir = Vector2.Distance(touchStartPos, vector);
                circlePos = vector;
            }
        }

        //タッチの終点を取得
        if (Input.GetMouseButtonUp(0)) {
            Vector2 touchEndPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            CircleDirection(touchStartPos.x + (circlePos.x - touchStartPos.x) / 2, touchStartPos.y + (circlePos.y - touchStartPos.y) / 2, (dir - 500) / 2, (dir + 500) / 2);
            GetDirection(touchEndPos.x - touchStartPos.x, touchEndPos.y - touchStartPos.y);
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

    //円の判定
    void CircleDirection(float midPointX, float midPointY, float minR, float maxR)
    {
        List<float> degree = new List<float>();
        degree.Clear();
        foreach (var pos in touchMovePos) {
            //ピタゴラスの定理 X*X + Y*Y = R*R => R = (X*X + Y*Y) / R
            float r = Mathf.Sqrt(Mathf.Pow(pos.x - midPointX, 2) + Mathf.Pow(pos.y - midPointY, 2));
            if (minR <= r && r <= maxR) {
                degree.Add(Mathf.Atan2(pos.y - midPointY, pos.x - midPointX) * Mathf.Rad2Deg);
                if (degree[degree.Count - 1] < 0) degree[degree.Count - 1] += 360;
            }
            else {
                isCircleCheck = false; 
            }
        }
        Debug.Log(degree[0] + "/" + degree[(degree.Count - 1) / 2] + "/" + degree[degree.Count - 1]);
        //もし180 => 90 => 0 or 360なら上右半回転
        if (160 <= degree[0] && degree[0] <= 200) {
            if (degree[(degree.Count - 1) / 2] <= degree[0])
                if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                    direction = "UpRightCircle";
        }

        //もし0 or 360 => 270 => 180なら下右半回転
        if (0 <= degree[0] && degree[0] <= 20 || 340 <= degree[0] && degree[0] <= 360) {
            if (0 <= degree[0] && degree[0] <= 20) {
                if (degree[(degree.Count - 1) / 2] <= degree[0] + 360)
                    if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                        direction = "DownRightCircle";
            }
            else if (340 <= degree[0] && degree[0] <= 360)
                if (degree[(degree.Count - 1) / 2] <= degree[0])
                    if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                        direction = "DownRightCircle";
        }

        //もし0 or 360 => 90 => 180なら上左半回転
        if (0 <= degree[0] && degree[0] <= 20 || 340 <= degree[0] && degree[0] <= 360) {
            if (0 <= degree[0] && degree[0] <= 20) {
                if (degree[0] <= degree[(degree.Count - 1) / 2] && degree[(degree.Count - 1) / 2] <= 180)
                    if(160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                        direction = "UpLeftCircle";
            }
            else if (340 <= degree[0] && degree[0] <= 360)
                if (degree[0] - 360 <= degree[(degree.Count - 1) / 2] && degree[(degree.Count - 1) / 2] <= 180)
                    if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                        direction = "UpLeftCircle";
        }

        //もし180 => 270 => 0 or 360なら下左半回転
        if (160 >= degree[0] && degree[0] <= 200) {
            if (degree[0] <= degree[(degree.Count - 1) / 2])
                if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                    direction = "DownLeftCircle";
        }

        //もし180 => 90 => 0 or 360 => 270 => 180なら右一回転
        //もし0 or 360 => 90 => 180 => 270 => 0 or 360なら左一回転
    }

    //フリックの方向を取得
    void GetDirection(float directionX, float directionY)
    {
        if (!isCircleCheck)
        {
            if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
            {
                if (30 < directionX) direction = "right";  //右向きにフリック
                else if (-30 > directionX) direction = "left";  //左向きにフリック
            }
            else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
            {
                if (30 < directionY) direction = "up";  //上向きにフリック
                else if (-30 > directionY) direction = "down";  //下向きのフリック
            }
            else direction = "touch";  //タッチを検出
        }

        DirectionCheck();
    }

    void DirectionCheck()
    {
        switch (direction) {
            case "UpRightCircle":
                Debug.Log("UpRightCircle");
                direction = null;
                break;

            case "DownRightCircle":
                Debug.Log("DownRightCircle");
                direction = null;
                break;

            case "UpLeftCircle":
                Debug.Log("UpLeftCircle");
                direction = null;
                break;

            case "DownLeftCircle":
                Debug.Log("DownLeftCircle");
                direction = null;
                break;

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
