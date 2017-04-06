using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Color : MonoBehaviour {
    //记录主角颜色
    public string Player_Color;
	//记录果子时限
    private int Exist_Time;
	//记录果子类型对应的数组位置
	private int Fruit_Type;
	//计时器
	private System.Timers.Timer t;

    float t_changecolor;
    private bool Time_Need;
    public GameObject[] fui;

	// Use this for initialization
    void Awake()
    {
        fui[0] = GameObject.FindGameObjectWithTag("fruit_cyan");
        fui[1] = GameObject.FindGameObjectWithTag("fruit_red");
        fui[2] = GameObject.FindGameObjectWithTag("fruit_purple");
        fui[3] = GameObject.FindGameObjectWithTag("fruit_orange");
        fui[4] = GameObject.FindGameObjectWithTag("fruit_yellow");
    }
	void Start () {
		t = new System.Timers.Timer (1000);
		t.Elapsed += new System.Timers.ElapsedEventHandler(Time_Start);

		Fruit_Type = 0;
		Exist_Time = 0;
        Time_Need = false;
    }

    void Update()
    {
        //检测游戏是否暂停
        //如果暂停，停止计时器
        if (GameObject.Find("UI").GetComponent<gamecontrol>().gaming)
        {
            t.Stop();
            Time_Need = true;
        }
        //如果没有暂停，并且计时器需要继续，开始计时器
        else if(!GameObject.Find("UI").GetComponent<gamecontrol>().gaming && Time_Need)
        {
            t.Start();
            Time_Need = false;
        }

        //如果果子存在时间为0，停止计时器，主角变白
        if (Exist_Time <= 0)
        {
            t.Stop();
            Player_Color = "White";
            GetComponent<Renderer>().material.color = UnityEngine.Color.white;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
		//快捷键1~5对应果子红、蓝、橙、绿、黄
		//按键使用果子变色
        if (GetComponent<playercontrol>().isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("ok");
                Fruit_Type = 0;
                EatFruit(Fruit_Type, new UnityEngine.Color((float)123 / 255, (float)207 / 255, (float)223 / 255));
                Player_Color = "Blue";
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Fruit_Type = 1;
                //吃掉果子后，果子数量减1，记录该种果子时限
                //改变果子颜色，color(r，g，b，a)定义颜色
                EatFruit(Fruit_Type, UnityEngine.Color.red);
                //记录主角颜色
                Player_Color = "Red";
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Fruit_Type = 2;
                EatFruit(Fruit_Type, UnityEngine.Color.magenta);
                Player_Color = "Purple";
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Fruit_Type = 3;
                EatFruit(Fruit_Type, UnityEngine.Color.green);
                Player_Color = "Orange";
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Fruit_Type = 4;
                EatFruit(Fruit_Type, UnityEngine.Color.yellow);
                Player_Color = "Yellow";
            }
        }

        if (Time.time >= t_changecolor)
            GetComponent<playercontrol>().ischanging = false;

	}

	//捡道具
	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.gameObject.tag == "Red") {
			GameObject.Find("UI").GetComponent<Prop>().fruits[1] += 1;
            fui[1].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[1].GetComponent<Image>().fillAmount = 1;
            fui[1].GetComponent<Image>().DOColor(UnityEngine.Color.red, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Blue") {
			GameObject.Find("UI").GetComponent<Prop>().fruits[0] += 1;
            fui[0].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[0].GetComponent<Image>().fillAmount = 1;
            fui[0].GetComponent<Image>().DOColor(new UnityEngine.Color((float)123 / 255, (float)207 / 255, (float)223 / 255), 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Purple") {
			GameObject.Find("UI").GetComponent<Prop>().fruits[2] += 1;
            fui[2].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[2].GetComponent<Image>().fillAmount = 1;
            fui[2].GetComponent<Image>().DOColor(UnityEngine.Color.magenta, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Orange") {
			GameObject.Find("UI").GetComponent<Prop>().fruits[3] += 1;
            fui[3].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[3].GetComponent<Image>().fillAmount = 1;
            fui[3].GetComponent<Image>().DOColor(UnityEngine.Color.green, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Yellow") {
			GameObject.Find("UI").GetComponent<Prop>().fruits[4] += 1;
            fui[1].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[1].GetComponent<Image>().fillAmount = 1;
            fui[1].GetComponent<Image>().DOColor(UnityEngine.Color.yellow, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Key") {
			GameObject.Find("UI").GetComponent<Prop>().key = true;
            Destroy(collider2D.gameObject);
		}
	}

	void EatFruit(int type, UnityEngine.Color player_color){
        //检查主角身上果子数量
		if (GameObject.Find("UI").GetComponent<Prop> ().fruits [type] > 0) {
            //吃掉一个果子，果子数量减1，记录该果子的存在时间
			GameObject.Find("UI").GetComponent<Prop> ().fruits [type] -= 1;
			Exist_Time = GameObject.Find("UI").GetComponent<Prop> ().fruits_time [type];

			if (GetComponent<playercontrol>().isGrounded)
            {
				GetComponent<playercontrol>().ischanging = true;
                t_changecolor = Time.time+0.2f;
            }

            //开始计时器，改变主角颜色
            t.Start();
            GetComponent<Renderer>().material.DOColor(player_color, 1.0f);

            if (fui[type].GetComponent<Image>().fillAmount > 0)
            {
                fui[type].GetComponent<Image>().DOFillAmount(0, Exist_Time + 1.0f);
            }
        }
	}

    //每过1秒，果子存在时间减1
	public void Time_Start(object source, System.Timers.ElapsedEventArgs e){
		Exist_Time -= 1;
        Debug.Log(Exist_Time);
    }
}
