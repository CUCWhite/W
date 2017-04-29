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
		//1=蓝 2=红 3=紫 4=绿 5=黄
		fruits = new int[5] { 0, 0, 0, 0, 0 }; //果子数量
		fruits_time = new int[5] { 5, 30, 7, 12, 9 };  //果子时限
	
		key = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
