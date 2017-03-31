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
		t.Elapsed += new System.Timers.ElapsedEventHandler(timing);

		Fruit_Type = 0;
		Exist_Time = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		//快捷键1~5对应果子蓝、红、紫、橙、黄
		//按键使用果子变色
        if (GetComponent<playercontrol>().isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
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

        if (Exist_Time <= 0)
        {
            t.Stop();
            Player_Color = "White";
            GetComponent<Renderer>().material.DOColor(UnityEngine.Color.white, 1.0f); 
        }

        if (Time.time >= t_changecolor)
            GetComponent<playercontrol>().ischanging = false;

	}

	//捡道具
	void OnTriggerEnter2D(Collider2D collider2D){
		if (collider2D.gameObject.tag == "Blue") {
			GetComponent<Prop>().fruits[0] += 1;
            fui[0].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[0].GetComponent<Image>().fillAmount = 1;
            fui[0].GetComponent<Image>().DOColor(new UnityEngine.Color((float)123 / 255, (float)207 / 255, (float)223 / 255), 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Red") {
			GetComponent<Prop>().fruits[1] += 1;
            fui[1].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[1].GetComponent<Image>().fillAmount = 1;
            fui[1].GetComponent<Image>().DOColor(UnityEngine.Color.red, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Purple") {
			GetComponent<Prop>().fruits[2] += 1;
            fui[2].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[2].GetComponent<Image>().fillAmount = 1;
            fui[2].GetComponent<Image>().DOColor(UnityEngine.Color.magenta, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Orange") {
			GetComponent<Prop>().fruits[3] += 1;
            fui[3].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[3].GetComponent<Image>().fillAmount = 1;
            fui[3].GetComponent<Image>().DOColor(UnityEngine.Color.green, 1.0f);
            Destroy(collider2D.gameObject);
		}
		if (collider2D.gameObject.tag == "Yellow") {
			GetComponent<Prop>().fruits[4] += 1;
            fui[1].GetComponent<Image>().color = UnityEngine.Color.white;
            fui[1].GetComponent<Image>().fillAmount = 1;
            fui[1].GetComponent<Image>().DOColor(UnityEngine.Color.yellow, 1.0f);
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

            if (GetComponent<playercontrol>().isGrounded)
            {
                GetComponent<playercontrol>().ischanging = true;
                t_changecolor = Time.time+0.2f;
            }
            if (fui[type].GetComponent<Image>().fillAmount > 0)
            {
                fui[type].GetComponent<Image>().DOFillAmount(0, Exist_Time + 1.0f);
            }
            t.Start();
            GetComponent<Renderer>().material.DOColor(player_color,1.0f);
        }
	}

	public void timing(object source, System.Timers.ElapsedEventArgs e){
		Exist_Time -= 1;
    }
}
