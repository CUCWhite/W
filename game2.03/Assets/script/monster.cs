using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {
	//巡逻长度
	public float length;
	//怪物的位置
	public float monster_x;
	public float monster_y;
    float hor;                                       //怪物水平方向位移差（用于判定切换移动怪物的移动方向）
    float xbefore;                                   //

    public bool lookingRight = true;                       //怪物的移动方向（怪物的朝向）

	// Use this for initialization
	void Start () {
		//设置怪物初始位置
		transform.position = new Vector3 (monster_x, monster_y, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//巡逻
        transform.position = new Vector3(monster_x + Mathf.PingPong(Time.time, length), monster_y, 0f);
		//动画
        if ((transform.position.x <= monster_x+0.05 && !lookingRight) || (transform.position.x >= monster_x+length-0.05 && lookingRight))               //判定主角的移动方向
            Flip();

	}

    void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
