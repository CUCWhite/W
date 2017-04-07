﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class playercontrol : MonoBehaviour {

    float maxSpeed = 5f;                             //player移动速度
    //float jumpForce = 350f;                          //player跳跃时给的力

    public bool isGrounded;                          //player是否在地上（用于判定不能进行二次跳跃）
    public bool isAlive;                             //player是否活着（用于判定游戏是否结束）
    bool isTop;                                      //player是否跳到最高点（用于判定是否播放顶点动画）
    float hor;                                       //player水平方向移动速度（用于判定切换待机和移动动画）
    float upordown;                                  //player竖直方向的移动方向（用于判定切换跳上和跳下动画）
    public bool ischanging;                          //是否需要变色

    public float SearchRadius;                       //碰撞盒的搜索范围
    public GameObject Pl_win;                  //player过关时生成的不可输入的预制体
    public GameObject door_shining;            //发光的门

    [HideInInspector]
    public bool lookingRight = true;                 //player的移动方向（主角的朝向）
    private Rigidbody2D rb2d;                        //player的刚体
    private Animator anim;                           //player的动画机

	// Use this for initialization
	void Start () {
        //main_mask.GetComponent<Renderer>().material.color = UnityEngine.Color.clear;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        isAlive = true;
        SearchRadius = 0.5f;
        anim.SetBool("Isalive", isAlive);

        ischanging = false;
        //Debug.Log(main_mask.GetComponent<Renderer>().material.color.a);
    }
    
    void Update ()
    {

    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            MoveHorizontal();
            Jump();
            SearchNearUnits();
           
            upordown = rb2d.velocity.y;
            anim.SetFloat("UporDown", upordown);
            if (upordown < 3.0 && upordown > -1.5)
                isTop = true;
            else isTop = false;
            anim.SetBool("Istop", isTop);
            anim.SetBool("Ischanging", ischanging);
            GameObject.Find("UI").GetComponent<gamecontrol>().gameover = false;
        }
        else
        {
            Destroy(this.gameObject, 0.7f);
            GameObject.Find("UI").GetComponent<gamecontrol>().gameover = true;
            GameObject.Find("UI").GetComponent<gamecontrol>().t_reload = 0.4f + Time.time;
        }
            
    }

    void MoveHorizontal()
    {
        //主角左右移动
        hor = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(hor));
        rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);
        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))               //判定主角的移动方向
            Flip();
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    void Jump()
    {
        //主角跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
            //rb2d.AddForce(new Vector2(0, jumpForce));
            rb2d.velocity = new Vector2(rb2d.velocity.x,6.7f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //主角在地面时，isGrounded=true，主角能够跳跃
        if (other.collider.tag == "ground")
        {
            isGrounded = true;
        }
        anim.SetBool("Isgrounded", isGrounded);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "monster_red":
                if(GetComponent<Color>().Player_Color != "Red")
                {
                    isAlive=false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "monster_blue":
                if (GetComponent<Color>().Player_Color != "Blue")
                {
                    isAlive = false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "monster_purple":
                if (GetComponent<Color>().Player_Color != "Purple")
                {
                    isAlive = false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "monster_orange":
                if (GetComponent<Color>().Player_Color != "Orange")
                {
                    isAlive = false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "monster_yellow":
                if (GetComponent<Color>().Player_Color != "Yellow")
                {
                    isAlive = false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "door":                   //过关大门
                if (isAlive&& GameObject.Find("UI").GetComponent<Prop>().key)
                {
                    GameObject.Find("UI").GetComponent<gamecontrol>().gameclear = true;
                    Pl_win.GetComponent<player_win>().pl_win_x = transform.position.x;
                    Pl_win.GetComponent<player_win>().pl_win_y = transform.position.y;
                    Instantiate(Pl_win, new Vector3(Pl_win.GetComponent<player_win>().pl_win_x, Pl_win.GetComponent<player_win>().pl_win_y, 0f), Quaternion.identity);
                    Destroy(this.gameObject);
                    Debug.Log("You Win");
                    
                    //过关特效
                    Instantiate(door_shining, GameObject.Find("神秘雕像未发光").transform.position-new Vector3(0.1f,0.1f,0), Quaternion.identity);
                    GameObject.Find("UI").GetComponent<gamecontrol>().t_reload = 1.2f + Time.time;
                    Destroy(GameObject.Find("神秘雕像未发光"));
                }
                break;
            case "ci":                    //碰触即死的障碍
                isAlive = false;
                anim.SetBool("Isalive", isAlive);
                break;
        }
    }

    void SearchNearUnits() //搜索某范围内的碰撞盒，用于判定player是否在地面上
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position-new Vector3(0,0.2f,0), SearchRadius,1 << LayerMask.NameToLayer("Ground"));
        if (colliders.Length <= 0)
        {
            isGrounded = false;
            anim.SetBool("Isgrounded", isGrounded);
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].tag == "ground")
                isGrounded = true;

        anim.SetBool("Isgrounded", isGrounded);
    }

}