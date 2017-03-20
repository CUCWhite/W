using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour {

    float maxSpeed = 5f;                             //player移动速度
    float jumpForce = 350f;                          //player跳跃时给的力

    bool isGrounded;                                 //player是否在地上（用于判定不能进行二次跳跃）
    public bool isAlive;                                    //player是否活着（用于判定游戏是否结束）
    bool isTop;                                      //player是否跳到最高点（用于判定是否播放顶点动画）
    float hor;                                      //player水平方向移动速度（用于判定切换待机和移动动画）
    float upordown;                                 //player竖直方向的移动方向（用于判定切换跳上和跳下动画）

    float t_dead;
   
    [HideInInspector]
    public bool lookingRight = true;                 //player的移动方向（主角的朝向）
    private Rigidbody2D rb2d;                        //player的刚体
    private Animator anim;                           //player的动画机

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        isAlive = true;
        anim.SetBool("Isalive", isAlive);
	}
    
    void Update ()
    {
        if (isAlive)
            Jump();
        else
            Destroy(this.gameObject, 0.6f);
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            MoveHorizontal();

            upordown = rb2d.velocity.y;
            anim.SetFloat("UporDown", upordown);                   //判定主角的上下状态
            if (upordown < 3.0 && upordown > -1.5)
                isTop = true;
            else isTop = false;
            anim.SetBool("Istop", isTop);
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
        if (Input.GetButtonDown("Jump")&&isGrounded)
            rb2d.AddForce(new Vector2(0, jumpForce));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //主角在地面时，isGrounded=true，主角能够跳跃
        if (other.collider.tag == "ground")
        {
            isGrounded = true;
        }
        anim.SetBool("Isgrounded", isGrounded);

        if (other.collider.tag == "Red")
            Destroy(other.gameObject);
    }
    void OnCollisionExit2D(Collision2D other)
    {
        //主角离开地面时，isGrounded=false，主角不能跳跃
        if (other.collider.tag == "ground")
            isGrounded = false;
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
            case "monster_black":
                if (GetComponent<Color>().Player_Color != "Black")
                {
                    isAlive = false;
                    anim.SetBool("Isalive", isAlive);
                }
                break;
            case "monster_green":
                if (GetComponent<Color>().Player_Color != "Green")
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
                
        }

    }

}
