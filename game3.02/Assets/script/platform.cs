using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    //浮台的位置
    public float platform_x;
    public float platform_y;

    public float speed;
    public float max_x;
    public float min_x;
    public float max_y;
    public float min_y;

    private GameObject player;
    float SearchRadius;
    public bool ifOnplatform;

    bool ifmove;

    // Use this for initialization
    void Start()
    {
        //设置浮台初始位置
        transform.position = new Vector3(platform_x, platform_y, 0f);

        player = GameObject.Find("White(Clone)");
        SearchRadius = 0.5f;
        ifOnplatform = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
            player = GameObject.Find("White(Clone)");

        SearchDownUnits();

        //浮台运动
        //if (ifmove)
        //{
            if (min_y != max_y)
            {
                if (transform.position.y >= max_y || transform.position.y <= min_y)
                {
                    speed = -speed;
                }
                transform.Translate(0f, speed, 0f);
                if (ifOnplatform)
                    player.transform.Translate(0f, speed, 0f);
            }
            if (min_x != max_x)
            {
                if (transform.position.x >= max_x || transform.position.x <= min_x)
                {
                    speed = -speed;
                }
                transform.Translate(speed, 0f, 0f);
                if (ifOnplatform)
                    player.transform.Translate(speed, 0f, 0f);
            }
        //}
        /*if (!ifmove)
        {
            if (transform.position.x <= max_x && transform.position.x >= min_x)
                transform.Translate(speed, 0f, 0f);
            if (transform.position.y <= max_y && transform.position.y >= min_y)
                transform.Translate(0f, speed, 0f);
        }*/
    }

    //以下代码的目的是让主角自觉跟随浮台运动
    void SearchDownUnits() //搜索某范围内的碰撞盒，用于判定player是否在地面上
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 0.2f, 0), SearchRadius, 1<<LayerMask.NameToLayer("Pl"));
        if (colliders.Length <= 0)
        {
            ifOnplatform = false;
            return;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                ifOnplatform = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            ifmove = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            ifmove = false;
    }

}
