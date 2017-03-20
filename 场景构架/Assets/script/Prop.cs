using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour {
	//存果子的数组
	public int[] fruits;
	public int[] fruits_time;
	//是否拿到钥匙
	public bool key;

	// Use this for initialization
	void Start () {
		//1=红 2=蓝 3=橙 4=绿 5=黄
		fruits = new int[5] { 0, 0, 0, 0, 0 }; //果子数量
		fruits_time = new int[5] { 5, 6, 7, 8, 9 };  //果子时限
	
		key = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
