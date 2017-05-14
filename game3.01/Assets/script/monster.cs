using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {
    //怪物的位置
    public float monster_x;
    public float monster_y;
    public float speed;
    public float max_x;
    public float min_x;

    public bool lookingRight;                       //怪物的移动方向（怪物的朝向）
    public bool m_run=false;                              //用于判定追赶动画是否播放
    public bool m_eat=false;                              //用于判定吃主角动画是否播放
    public bool m_playersafe=true;                             //用于判定主角是否安全

    private GameObject player;
    public float border_max_x;
    public float border_min_x;
    public float border_y;
    public float platform_y;

    
    // Use this for initialization
    void Start() {
        //设置怪物初始位置
        player = GameObject.Find("White(Clone)");
        transform.position = new Vector3(monster_x, monster_y, 0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (player == null)
            player = GameObject.Find("White(Clone)");

        if(!m_run&&!m_eat)
            Xunluo();

        //主角进入左侧平台后
        if (player.GetComponent<playercontrol>().isAlive
            && player.transform.position.x <= border_max_x
            && player.transform.position.x >= border_min_x
            && player.transform.position.y >= platform_y)
        {
            //如果主角被怪物看到
            if (lookingRight)
            {
                if (transform.position.x - player.transform.position.x <= -1.0f && transform.position.x - player.transform.position.x >= -4.0f)
                {
                    //并且主角没有变色
                    if (!player.GetComponent<playercontrol>().issuited)
                        PlayerDied();
                    //或者主角跳出草丛
                    else if (player.transform.position.y >= border_y)
                        PlayerDied();
                    else
                    {
                        player.GetComponent<playercontrol>().player_safe = true;
                        m_run = m_eat = false;
                    }
                }
            }
            else
            {
                if (transform.position.x - player.transform.position.x >= 1.0f && transform.position.x - player.transform.position.x <= 4.0f)
                {
                    if (!player.GetComponent<playercontrol>().issuited)
                        PlayerDied();
                    else if (player.transform.position.y >= border_y)
                        PlayerDied();
                    else
                    {
                        player.GetComponent<playercontrol>().player_safe = true;
                        m_run = m_eat = false;
                    }
                }
            }
        }
        else m_run = m_eat = false;

        if (!m_eat && m_run)
        {
            transform.Translate(new Vector3((player.transform.position.x - transform.position.x) * 0.03f, 0f, 0f));
            if (player.transform.position.x < border_min_x)
                m_eat = m_run = false;
        }
    }

    void Xunluo()
    {
        if (transform.position.x >= max_x || transform.position.x <= min_x)
        {
            Vector3 myScale = transform.localScale;
            myScale.x *= -1;
            transform.localScale = myScale;
            lookingRight = !lookingRight;
            speed = -speed;
        }
        transform.Translate(speed, 0f, 0f);
    }

    void PlayerDied()
    {
        //怪物扑过去动画
        player.GetComponent<playercontrol>().player_safe = false; 
        m_run = true;
        GetComponent<Animator>().SetBool("Runing", m_run);
    }
}

