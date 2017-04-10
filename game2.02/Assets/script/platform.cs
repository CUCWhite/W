using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {
    //浮动高度
    public float height;
    //浮台的位置
    public float platform_x;
    public float platform_y;

    // Use this for initialization
    void Start () {
        //设置浮台初始位置
        transform.position = new Vector3(platform_x, platform_y, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        //浮台运动
        transform.position = new Vector3(platform_x, platform_y + Mathf.PingPong(Time.time, height), 0f);
    }
}
