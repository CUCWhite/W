using UnityEngine;
using System.Collections;

public class monster : MonoBehaviour {
	//巡逻长度
	public float length;
	//怪物的位置
	public float monster_x;
	public float monster_y;

	// Use this for initialization
	void Start () {
		length = 1f;
		monster_x = 1.5f;
		monster_y = -2.5f;

		//设置怪物初始位置
		transform.position = new Vector3 (monster_x, monster_y, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//巡逻
        transform.position = new Vector3(monster_x + Mathf.PingPong(Time.time, length), monster_y, 0f);

		//动画

	}
}
