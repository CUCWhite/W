using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour {


    float maxSpeed = 5f;                             //player移动速度
    float jumpForce = 350f;                          //player跳跃时给的力
   
    int eatcolornum;                                 //player吃下的果子编号-1红-2黄-3蓝-4绿-5紫
    int playercolornum;                              //player现在的颜色编号-1红-2黄-3蓝-4绿-5紫-6白

    public bool isGrounded = false;                         //player是否在地上（用于判定不能进行二次跳跃）

    [HideInInspector]
    public bool lookingRight = true;                 //player的移动方向（主角的朝向）
    private Rigidbody2D rb2d;                        //player的刚体
    private Animator anim;                           //player的动画机

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //主角跳跃
        if (Input.GetButtonDown("Jump")&&isGrounded)
            rb2d.AddForce(new Vector2(0, jumpForce));
    }

    void FixedUpdate()
    {
        //主角左右移动
        float hor = Input.GetAxis("Horizontal");
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

    void OnCollisionEnter2D(Collision2D other)
    {
        //主角在地面时，isGrounded=true，主角能够跳跃
        if (other.collider.tag == "ground")
            isGrounded = true;
        else isGrounded = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //主角离开地面时，isGrounded=false，主角不能跳跃
        if (other.collider.tag == "ground")
            isGrounded = false;
        else isGrounded = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="red")
        {

        }
        if (other.tag == "key")
        {

        }
        if (other.tag == "monster")
        {

        }
    }

}
