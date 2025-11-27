using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform midBg;
    public Transform bottomBg;
    public Transform topBg;
    public float height = 10f;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        topBg.position += Vector3.down * speed * Time.deltaTime;
        midBg.position += Vector3.down * speed * Time.deltaTime;
        bottomBg.position += Vector3.down * speed * Time.deltaTime;
        if (bottomBg.position.y <= -height)
        {
            SwapBG();
        }
    }
    void SwapBG()
    {
        bottomBg.position = topBg.position + Vector3.up * height;
        Transform temp = bottomBg;
        bottomBg = midBg;
        midBg = topBg;
        topBg = temp;
    }
}
