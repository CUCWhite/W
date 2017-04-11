using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_fly : MonoBehaviour {
    public GameObject player;
    private float border_x;
    public float border_y;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("White(Clone)");
        border_x = -2.5f;
        border_y = 3.8f;
	}
	
	// Update is called once per frame
	void Update () {
        //主角进入左侧平台后
        if (player.GetComponent<playercontrol>().isAlive && player.transform.position.x <= border_x)
        {
            if (player.transform.position.y >= 2.6f)
            {
                //如果主角被怪物看到
                if (GetComponent<monster>().lookingRight)
                {
                    if (transform.position.x - player.transform.position.x <= 0.5f)
                    {
                        //并且主角没有变色
                        if (player.GetComponent<Color>().Player_Color != "Red")
                        PlayerDied();
                        //或者主角跳出草丛
                        else if (player.transform.position.y >= border_y)
                            PlayerDied();
                    }
                }
                else
                {
                    if (transform.position.x - player.transform.position.x >= 0.5f)
                    {
                        if (player.GetComponent<Color>().Player_Color != "Red") 
                        PlayerDied();
                        else if (player.transform.position.y >= border_y)
                            PlayerDied();
                    }
                }
            }
        }
	}

    void PlayerDied()
    {
        //怪物扑过去动画
        //主角死亡
        player.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("sounds/PlayerDied");
        player.GetComponent<AudioSource>().Play();
        player.GetComponent<playercontrol>().isAlive = false;
        player.GetComponent<Animator>().SetBool("Isalive", player.GetComponent<playercontrol>().isAlive);
    }
}
