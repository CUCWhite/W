using UnityEngine;
using System.Collections;

public class Color : MonoBehaviour {
    //记录主角颜色
    public string Player_Color;
	//记录果子时限
    private int Exist_Time;
	//记录果子类型对应的数组位置
	private int Fruit_Type;
	//计时器
	private System.Timers.Timer t; 

	// Use this for initialization
	void Start () {
		t = new System.Timers.Timer (1000);
		t.Elapsed += new System.Timers.ElapsedEventHandler(timing);

		Fruit_Type = 0;
		Exist_Time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//快捷键1~5对应果子红、蓝、橙、绿、黄
		//按键使用果子变色
        if (GetComponent<playercontrol>().isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Fruit_Type = 0;
                //吃掉果子后，果子数量减1，记录该种果子时限
                //改变果子颜色，color(r，g，b，a)定义颜色
                EatFruit(Fruit_Type, UnityEngine.Color.red);
                //记录主角颜色
                Player_Color = "Red";
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Fruit_Type = 1;
                EatFruit(Fruit_Type, new UnityEngine.Color((float)26 / 255, (float)254 / 255, (float)255 / 255));
                Player_Color = "Blue";
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Fruit_Type = 2;
                EatFruit(Fruit_Type, UnityEngine.Color.black);
                Player_Color = "Black";
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Fruit_Type = 3;
                EatFruit(Fruit_Type, UnityEngine.Color.green);
                Player_Color = "Green";
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Fruit_Type = 4;
                EatFruit(Fruit_Type, UnityEngine.Color.yellow);
                Player_Color = "Yellow";
            }
        }

        if (Exist_Time <= 0)
        {
            t.Stop();
            Player_Color = "White";
            GetComponent<Renderer>().material.color = UnityEngine.Color.white;
            //Debug.Log (Exist_Time);
        }
        
	}

	//捡道具
	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.gameObject.tag == "Red") {
			GetComponent<Prop>().fruits[0] += 1;
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Blue") {
			GetComponent<Prop>().fruits[1] += 1;
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Black") {
			GetComponent<Prop>().fruits[2] += 1;
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Green") {
			GetComponent<Prop>().fruits[3] += 1;
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Yellow") {
			GetComponent<Prop>().fruits[4] += 1;
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Key") {
            GetComponent<Prop>().key = true;
            Destroy(collider2D.gameObject);
		}
	}

	void EatFruit(int type, UnityEngine.Color player_color){
		if (GetComponent<Prop> ().fruits [type] > 0) {
			GetComponent<Prop> ().fruits [type] -= 1;
			Exist_Time = GetComponent<Prop> ().fruits_time [type];

            t.Start();
            GetComponent<Renderer>().material.color = player_color; 

			Debug.Log ("OK");
		} else
			Debug.Log ("NO");
	}

	public void timing(object source, System.Timers.ElapsedEventArgs e){
		Exist_Time -= 1;
		//Debug.Log (Exist_Time);
	}
}
