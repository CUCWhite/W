using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TeachOne : MonoBehaviour {
    public Text[] teach;
    public Text[] sbt;
    public Image[] keypic;
    public static bool[] cankeydown;
    public static bool[] iskeydown;
	// Use this for initialization
    void Awake()
    {
        cankeydown = new bool[5];
        iskeydown = new bool[5];

    }
	void Start () {
		for(int i=0;i<5;i++)
        {
            cankeydown[i] = false;
        }
        for (int j = 0; j < 5; j++)
        {
            iskeydown[j] = false;
        }
        for (int k = 0; k < 4; k++)
        {
            teach[k].color = UnityEngine.Color.clear;
        }
        for (int n = 0; n < 3; n++)
        {
            sbt[n].color = UnityEngine.Color.clear;
        }
        for (int m = 0; m < 8; m++)
        {
            keypic[m].color = UnityEngine.Color.clear;
        }
        cankeydown[0] = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!iskeydown[0])
        {
            teach[0].DOBlendableColor(UnityEngine.Color.white, 3.0f);
            keypic[0].DOColor(UnityEngine.Color.white, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)&&!iskeydown[0])
        {
            iskeydown[0] = true;
            keypic[0].gameObject.SetActive(false);
            keypic[4].DOColor(UnityEngine.Color.white,0.01f);
            if (keypic[4])
            {
                Destroy(keypic[4], 0.5f);
            }
            //teach[0].DOColor(UnityEngine.Color.clear, 0.5f);
            //teach[0].color = UnityEngine.Color.clear;
            teach[0].gameObject.SetActive(false);
            cankeydown[1] = true;
        }
        if(cankeydown[1]&&!iskeydown[1])
        {
            teach[1].DOBlendableColor(UnityEngine.Color.white, 3.0f);
            keypic[1].DOColor(UnityEngine.Color.white, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && !iskeydown[1]&&iskeydown[0])
        {
            iskeydown[1] = true;
            teach[1].gameObject.SetActive(false);
            sbt[0].color= UnityEngine.Color.clear;
            keypic[1].gameObject.SetActive(false);
            keypic[5].DOColor(UnityEngine.Color.white, 0.01f);
            if (keypic[5])
            {
                Destroy(keypic[5], 0.5f);
            }
        }
        if (cankeydown[2]&&!iskeydown[2])
        {
            sbt[0].DOColor(UnityEngine.Color.white, 0.5f);
            
            Invoke("clear0",1.0f);
        }


        if (iskeydown[2] && cankeydown[3] && !iskeydown[3])
        {
            sbt[1].DOColor(UnityEngine.Color.white, 1.0f);
            Invoke("clear1", 1.0f);
        }
        if(iskeydown[4])
        {
            GameObject.Find("Teach").SetActive(false);
        }
    }
    void clear0()
    {
        sbt[0].CrossFadeAlpha(0.0f, 1.0f, false);
        Invoke("showsub",1.5f);
       
    }
    void showsub()
    {
        sbt[0].gameObject.SetActive(false);
        if(!iskeydown[2])
        {
            teach[2].DOBlendableColor(UnityEngine.Color.white, 3.0f);
            keypic[2].DOColor(UnityEngine.Color.white, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !iskeydown[2])
        {
            iskeydown[2] = true;
            teach[2].gameObject.SetActive(false);
            keypic[2].gameObject.SetActive(false);
            keypic[6].DOColor(UnityEngine.Color.white, 0.01f);
            if (keypic[6])
            {
                Destroy(keypic[6], 0.5f);
            }
        }
    }
    void clear1()
    {
        sbt[1].CrossFadeAlpha(0.0f, 1.0f, false);
        Invoke("showteach",1.5f);
    }
    void showteach()
    {
        sbt[1].gameObject.SetActive(false);
        if(!iskeydown[3])
        {
            teach[3].DOBlendableColor(UnityEngine.Color.white, 3.0f);
            keypic[3].DOColor(UnityEngine.Color.white, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !iskeydown[3])
        {
            iskeydown[3] = true;
            teach[3].gameObject.SetActive(false);
            sbt[2].DOColor(UnityEngine.Color.white, 1.0f);
            keypic[3].gameObject.SetActive(false);
            keypic[7].DOColor(UnityEngine.Color.white, 0.01f);
            if (keypic[7])
            {
                Destroy(keypic[7], 0.5f);
            }
            Invoke("stopall", 2.0f);
        }
    }
    void stopall()
    {
        sbt[2].CrossFadeAlpha(0.0f, 3.0f, false);
    }
}
