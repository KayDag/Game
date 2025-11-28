using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class UserSavePointDatas
{
    //lưu ds điểm qua các lần chơi
    public List<int> points;

    public UserSavePointDatas()
    {
        points = new List<int>();
    }
    //đầu game
    public void StartGame()
    {
            points.Add(0);
    }
    //sửa điểm hiện tại
    public void UpdatePoints(int point)
    {
        //points.Last()
        if (points.Count > 0)
        {
            points[points.Count - 1] = point;
        }
    }
}
