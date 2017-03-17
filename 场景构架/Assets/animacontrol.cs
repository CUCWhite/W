using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacontrol : MonoBehaviour {

    //public GameObject player;
    Animator anim;
    public static int ani_state;
    float t;

	// Use this for initialization
	void Start () {
        ani_state = 0;
        anim = GetComponentInChildren<Animator>();
        t = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetInteger("state", ani_state);   //使ani_state的值就是动画编辑器中state的值
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", h * h + v * v);
        Debug.Log(h * h + v * v);

        if (Input.GetKeyDown(KeyCode.Space))      //上下
        {
            ani_state = 1;
            t = Time.time + 0.4f;
        }
        
        if ((Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.A))&&GetComponent<playercontrol>().isGrounded)             
        {
            ani_state = 0;
        }

        
        if (ani_state == 1)
            if (Time.time >= t)
            {
                ani_state = 0;
            }
		
	}
}
