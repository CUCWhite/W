using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_fly : MonoBehaviour {
    //怪物的位置
    public float monster_x;
    public float monster_y;
    public float speed;
    public float max_x;
    public float min_x;
    float t_monsterpause;        //怪物停顿时间
    float t_monsterfirestop;     //怪物停止吐火球动画
    bool changex = false;        //用于怪物转向
    bool firing=false;                 //用于怪物喷火动画判定
    bool fired;                //怪物是否已经喷过火球
    bool fireoneball;            //用于让怪物只喷一个火球;

    public bool lookingRight;                       //怪物的移动方向（怪物的朝向）
    public GameObject fireballs;                    //怪物喷出的火球

    private GameObject player;
    public float border_x;
    public float border_y;
    public float platform_y;

    // Use this for initialization
    void Start () {
        //设置怪物初始位置
        transform.position = new Vector3(monster_x, monster_y, 0f);
        t_monsterpause = Time.time;
        t_monsterfirestop = Time.time;

        border_x = -2.5f;
        border_y = 3.8f;
        platform_y = 2.6f;

        fired = false;
        fireoneball = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player == null)
        {
            player = GameObject.Find("White(Clone)");
        }
        //巡逻
        //转向
        if (transform.position.x > max_x)
        {
            t_monsterpause = Time.time + 1.0f;
            transform.position = new Vector3(max_x, transform.position.y, transform.position.z);
            changex = true;
        }
        if (transform.position.x < min_x)
        {
            t_monsterpause = Time.time + 1.0f;
            transform.position = new Vector3(min_x, transform.position.y, transform.position.z);
            changex = true;
        }

        if (Time.time > t_monsterpause&&!firing)
        {
            if (changex)
            {
                Vector3 myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                lookingRight = !lookingRight;
                speed = -speed;
                changex = false;
            }
            transform.Translate(speed, 0f, 0f);
        }

        //主角进入左侧平台后
        if (player.GetComponent<playercontrol>().isAlive
            && player.transform.position.x <= border_x
            && player.transform.position.y >= platform_y)
        {
            //如果主角被怪物看到
            if (lookingRight)
            {
                if (transform.position.x - player.transform.position.x <= -0.5f)
                {
                    //并且主角没有变色
                    if (!player.GetComponent<playercontrol>().issuited)
                    { 
                        firing = true;
                        GetComponent<Animator>().SetBool("Firing", firing);
                        PlayerDied();
                    }
                    //或者主角跳出草丛
                    else if (player.transform.position.y >= border_y)
                    {
                        firing = true;
                        GetComponent<Animator>().SetBool("Firing", firing);
                        PlayerDied();
                    }
                }
            }
            else
            {
                if (transform.position.x - player.transform.position.x >= 0.5f)
                {
                    if (!player.GetComponent<playercontrol>().issuited)
                    {
                        firing = true;
                        GetComponent<Animator>().SetBool("Firing", firing);
                        PlayerDied();
                    }
                    else if (player.transform.position.y >= border_y)
                    {
                        firing = true;
                        GetComponent<Animator>().SetBool("Firing", firing);
                        PlayerDied();
                    }
                }
            }
        }
        //Debug.Log(fired);
        GetComponent<Animator>().SetBool("Fired", fired);
    }

    void PlayerDied()
    {
        if (!fireoneball)
        {
            //口吐火球
            if (Instantiate(fireballs, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity))
            {
                t_monsterfirestop =0.2f + Time.time;
                fireoneball = true;
            }
        }
    }
}
