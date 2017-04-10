using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gamecontrol : MonoBehaviour {

    public GameObject Pl;
    public Vector3 firstposition;                     //主角在关卡的初始生成位置

    public bool gaming;                               //游戏是否暂停
    public bool gameclear;                            //是否过关
    public bool gameover;                             //游戏是否结束（玩家是否死亡）
    private static int gamelevel=1;                   //游戏进行到哪一关
    string lname;                                     //用于记录该关的名字

    bool ifchangecamera;                              //是否能够切换摄像机
    public float t_reload;                            //用于延后载入新场景的时间，使死亡动画和过关动画能够播完
    string sceneName;
	// Use this for initialization
    void Awake()
    {
        SceneManager.GetSceneByName(sceneName);
    }
	void Start () {
        
        //firstposition = new Vector3(-7f,4f,0f);
        Instantiate(Pl, firstposition, Quaternion.identity);
        if (sceneName == "level1")
        {
            Pl.transform.localScale = new Vector3(-7f, 4f, 0);
        }
        gaming = false;
        gameover = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(gameclear)                                 //通关后载入下一关卡
            Nextlevel();

         if (Input.GetKeyDown(KeyCode.F))             //摁F键暂停游戏
            PauseGame();

        if (gameover)    //玩家死亡后自动重载该关卡
            Restart();


    }

    void PauseGame()            //暂停游戏
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

    void Restart()             //重新开始游戏
    {
        if (Time.time > t_reload)
        {
            lname = "level" + gamelevel.ToString();
            Application.LoadLevel(lname);
        }
    }

    void Nextlevel()           //进行下一关
    {
        if (Time.time > t_reload)
        {
            gamelevel = gamelevel + 1;
            lname = "level" + gamelevel.ToString();
            Application.LoadLevel(lname);
        }
    }
}
