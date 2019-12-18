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
    bool isCircleCheck;
    static public bool _trickcheck = false;
    float dir;

    void Update()
    {
        if (_trickcheck) {
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
            isCircleCheck = true;
        }

        //タッチの中点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Moved) {
        Vector2 vector = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            touchMovePos.Add(vector);
            if (dir < Vector2.Distance(touchStartPos, vector)) {
                dir = Vector2.Distance(touchStartPos, vector);
                circlePos = vector;
            }
        }

        //タッチの終点を取得
        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            Vector2 touchEndPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            CircleDirection(touchStartPos.x + (circlePos.x - touchStartPos.x) / 2, touchStartPos.y + (circlePos.y - touchStartPos.y) / 2, (dir - 500) / 2, (dir + 500) / 2);
            GetDirection(touchEndPos.x - touchStartPos.x, touchEndPos.y - touchStartPos.y);
        }
            #endregion
#endif
        }
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
        }

        #region 上右半回転
        if (160 <= degree[0] && degree[0] <= 200)
        {
            if (degree[(degree.Count - 1) / 2] <= degree[0])
                if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                    direction = "UpRightCircle";
        }
        #endregion

        #region 下右半回転
        if (0 <= degree[0] && degree[0] <= 20)
        {
            if (degree[(degree.Count - 1) / 2] <= degree[0] + 360)
                if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                    direction = "DownRightCircle";
        }
        else if (340 <= degree[0] && degree[0] <= 360)
        {
            if (degree[(degree.Count - 1) / 2] <= degree[0])
                if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                    direction = "DownRightCircle";
        }
        #endregion

        #region 上左半回転
        if (0 <= degree[0] && degree[0] <= 20)
        {
            if (degree[0] <= degree[(degree.Count - 1) / 2] && degree[(degree.Count - 1) / 2] <= 180)
                if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                    direction = "UpLeftCircle";
        }
        else if (340 <= degree[0] && degree[0] <= 360)
        {
            if (degree[0] - 360 <= degree[(degree.Count - 1) / 2] && degree[(degree.Count - 1) / 2] <= 180)
                if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                    direction = "UpLeftCircle";
        }
        #endregion

        #region 下左半回転
        if (160 <= degree[0] && degree[0] <= 200)
        {
            if (degree[0] <= degree[(degree.Count - 1) / 2])
                if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                    direction = "DownLeftCircle";
        }
        #endregion

        #region 右一回転
        if (160 <= degree[0] && degree[0] <= 200)
        {
            if (degree[0] >= degree[(degree.Count - 1) / 4])
                if (degree[(degree.Count - 1) / 4] >= degree[(degree.Count - 1) / 2])
                {
                    if (360 >= degree[(degree.Count - 1) * 3 / 4])
                        if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                            direction = "RightCircle";
                }
                else if (360 >= degree[(degree.Count - 1) / 2])
                {
                    if (degree[(degree.Count - 1) / 2] >= degree[(degree.Count - 1) * 3 / 4])
                        if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                            direction = "RightCircle";
                }
        }
        if (0 <= degree[0] && degree[0] <= 20)
        {
            if (degree[(degree.Count - 1) / 4] <= 360)
                if (degree[(degree.Count - 1) / 2] <= degree[(degree.Count - 1) / 4])
                    if (degree[(degree.Count - 1) * 3 / 4] <= degree[(degree.Count - 1) / 2])
                        if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                            direction = "RightCircle";
        }
        else if (340 <= degree[0] && degree[0] <= 360)
        {
            if (degree[(degree.Count - 1) / 4] <= degree[0])
                if (degree[(degree.Count - 1) / 2] <= degree[(degree.Count - 1) / 4])
                    if (degree[(degree.Count - 1) * 3 / 4] <= degree[(degree.Count - 1) / 2])
                        if (0 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 20 || 340 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 360)
                            direction = "RightCircle";
        }
        #endregion

        #region 左一回転
        if (160 <= degree[0] && degree[0] <= 200)
            if (degree[0] <= degree[(degree.Count - 1) / 4])
            {
                if (degree[(degree.Count - 1) / 4] <= degree[(degree.Count - 1) / 2])
                {
                    if (0 <= degree[(degree.Count - 1) * 3 / 4])
                        if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                            direction = "LeftCircle";
                }
                else if (degree[(degree.Count - 1) / 2] <= 20)
                {
                    if (degree[(degree.Count - 1) / 2] <= degree[(degree.Count - 1) * 3 / 4])
                        if (160 <= degree[degree.Count - 1] && degree[degree.Count - 1] <= 200)
                            direction = "LeftCircle";
                }
            }
        if (0 <= degree[0] && degree[0] <= 20)
        {
            if (degree[0] <= degree[(degree.Count - 1) / 4])
                if (degree[(degree.Count - 1) / 4] <= degree[(degree.Count - 1) / 2])
                    if (degree[(degree.Count - 1) / 2] <= degree[(degree.Count - 1) * 3 / 4])
                        if (degree[(degree.Count - 1) * 3 / 4] <= degree[degree.Count - 1] || degree[degree.Count - 1] <= 20)
                            direction = "LeftCircle";
        }
        else if (340 <= degree[0] && degree[0] <= 360)
        {
            if (0 <= degree[(degree.Count - 1) / 4])
                if (degree[(degree.Count - 1) / 4] <= degree[(degree.Count - 1) / 2])
                    if (degree[(degree.Count - 1) / 2] <= degree[(degree.Count - 1) * 3 / 4])
                        if (degree[(degree.Count - 1) * 3 / 4] <= degree[degree.Count - 1] || degree[degree.Count - 1] <= 20)
                            direction = "LeftCircle";
        }
        #endregion
    }

    //フリックの方向を取得
    void GetDirection(float directionX, float directionY)
    {
        if (direction == null) {
            if (30 <= directionX) {
                if (30 <= directionY) direction = "rightup";                     //右上向きにフリック
                else if (-30 >= directionY) direction = "rightdown";         //右下向きにフリック
                else direction = "right";                                                 //右向きにフリック
            }
            else if (-30 >= directionX) {
                if (30 <= directionY) direction = "leftup";                        //左上向きにフリック
                else if (-30 >= directionY) direction = "leftdown";            //左下向きにフリック
                else direction = "left";                                                    //左向きにフリック
            }
            else {
                if (30 <= directionY) direction = "up";                             //上向きにフリック
                else if (-30 >= directionY) direction = "down";                 //下向きのフリック
                else direction = "touch";                                                 //タッチを検出
            }
        }

        DirectionCheck();
    }

    void DirectionCheck()
    {
        TrickData.Trick(direction);
        direction = null;
    }
}
