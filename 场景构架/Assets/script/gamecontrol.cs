using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gamecontrol : MonoBehaviour {

    public GameObject Pl;
    public Vector3 firstposition;                     //主角在关卡的初始生成位置
    bool gaming;                                      //游戏是否正在进行中（判断游戏是否暂停）
    public bool gameclear;                            //是否过关

	// Use this for initialization
	void Start () {
        firstposition = new Vector3(-7f,4f,0f);
        Instantiate(Pl,firstposition, Quaternion.identity);
        gaming = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetKeyDown(KeyCode.M))
        //    Instantiate(Pl, firstposition, Quaternion.identity);

        if(gameclear)
        {
            //load新场景

        }

        PauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!gaming)
            {
                Time.timeScale = 0;
                gaming = !gaming;
            }
            else if (gaming)
            {
                Time.timeScale = 1;
                gaming = !gaming;
            }

            Debug.Log("PAUSE");
        }
    }
}
