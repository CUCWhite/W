using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {
    //浮台的位置
    public float platform_x;
    public float platform_y;

    public float speed;
    public float max_y;
    public float min_y;

    // Use this for initialization
    void Start () {
        //设置浮台初始位置
        transform.position = new Vector3(platform_x, platform_y, 0f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //浮台运动
        if (transform.position.y >= max_y || transform.position.y <= min_y)
            speed = -speed;

        transform.Translate(0f, speed, 0f);
    }
}
