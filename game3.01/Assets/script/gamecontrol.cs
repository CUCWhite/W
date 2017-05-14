using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using Mono.Data.Sqlite;

public class gamecontrol : MonoBehaviour {

    public GameObject Player;
    public Vector3 Player_Pos;                     //主角在关卡的初始生成位置

    public bool gaming;                               //游戏是否暂停
    public bool gameclear;                            //是否过关
    public bool gameover;                             //游戏是否结束（玩家是否死亡）
    private static int gamelevel;                   //游戏进行到哪一关
    public int level;
    private int maxlevel;                           //游戏最大关卡
    string lname;                                     //用于记录该关的名字
    public Image pauseUI;
    bool ifchangecamera;                              //是否能够切换摄像机
    public float t_reload;                            //用于延后载入新场景的时间，使死亡动画和过关动画能够播完
    string sceneName;
    public Button startbutton;

    // Use this for initialization
    void Awake()
    {
        sceneName = Application.loadedLevelName;
    }
	void Start ()
    {
        //pauseUI.enabled = false;
        pauseUI.gameObject.SetActive(false);
        gaming = false;
        gameover = false;
        maxlevel = 3;

        //调试用，对应每一个关卡主角位置
        for (int i = 1; i <= maxlevel; i++)
        {
            string s = "level" + i.ToString();
            if (sceneName == s)
            {
                gamelevel = i;
                level = i;
                break;
            }
        }
        //生成主角
        //InitPlayer();

        switch (gamelevel)
        {
            case 1:
                Instantiate(Player, new Vector3(-7f, 4f, 0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(Player, new Vector3(-6.2f, -0.8f, 0f), Quaternion.identity);
                break;
            case 3:
                Instantiate(Player, new Vector3(-6.2f, 4f, 0f), Quaternion.identity);
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
         if (Input.GetKeyDown(KeyCode.Escape))             //摁F键暂停游戏
            PauseGame();

        if (gameover || gameclear)                     //过关或者死亡时，重载关卡
            LoadLevel();
    }

    void PauseGame()            //暂停游戏
    {
        if (!gaming)
        {
            pauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
            gaming = !gaming;
            startbutton.Select();
        }
        else if (gaming)
        {
            pauseUI.gameObject.SetActive(false);
            Time.timeScale = 1;
            gaming = !gaming;
        }
        Debug.Log("PAUSE");
    }

    void LoadLevel()           //进行下一关
    {
        if (Time.time > t_reload)
        {
            if (gameclear)
            {
                if (gamelevel < maxlevel)
                {
                    gamelevel += 1;
                    //UpatePlayerState(gamelevel);
                }
                else if(gamelevel == maxlevel)
                {
                    //UpatePlayerState(1);
                    gamelevel = 1;
                }
                gameclear = false;
            }
            
            lname = "level" + gamelevel.ToString();
            SceneManager.LoadScene(lname);

            gameover = false;
        }
    }

    //初始化主角
    void InitPlayer()
    {
        //创建数据库名称为xuanyusong.db
        DbAccess db = new DbAccess("data source = White.db");

        //读取数据库表中的数据
        //读取主角数据
        SqliteDataReader Read_Player = db.SelectWhere("GameCtrl", new string[] { "*" }, new string[] { "Gstate" }, new string[] { "=" }, new string[] { "1" });
        while (Read_Player.Read())
        {
            //记录的关卡数以及主角位置
            gamelevel = Read_Player.GetInt32(Read_Player.GetOrdinal("Gchapter"));
            Player_Pos = new Vector3(Read_Player.GetFloat(Read_Player.GetOrdinal("Gposx")), Read_Player.GetFloat(Read_Player.GetOrdinal("Gposy")), 0f);
            //生成主角
            if (gamelevel == 1)
                Player.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            if (gamelevel > 1)
                Player.transform.localScale = new Vector3(0.4f, 0.4f, 1);
            Instantiate(Player, Player_Pos, Quaternion.identity);
        }
        Read_Player.Dispose();

        //关闭对象
        db.CloseSqlConnection();
    }

    //更新存档信息
    void UpatePlayerState(int level2)
    {
        DbAccess db = new DbAccess("data source = White.db");

        //找到主角存档
        SqliteDataReader Read_Player = db.SelectWhere("GameCtrl", new string[] { "*" }, new string[] { "Gstate" }, new string[] { "=" }, new string[] { "1" });
        while (Read_Player.Read())
        {
            //更新存档信息
            int level1 = Read_Player.GetInt32(Read_Player.GetOrdinal("Gchapter"));
            //修改旧存档标记 Gstate=0
            string query0 = "UPDATE GameCtrl SET Gstate = 0 WHERE Gchapter = " + level1;
            SqliteDataReader Update_Player0 = db.ExecuteQuery(query0);
            Update_Player0.Dispose();

            //建立新存档标记 Gstate=1
            string query1 = "UPDATE GameCtrl SET Gstate = 1 WHERE Gchapter = " + level2;
            SqliteDataReader Update_Player1 = db.ExecuteQuery(query1);
            Update_Player1.Dispose();
        }
        Read_Player.Dispose();

        db.CloseSqlConnection();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("guide");
    }
    public void BacktoGame()
    {
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        gaming = !gaming;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        string s = "level" + level.ToString();
        SceneManager.LoadScene(s);
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        gaming = !gaming;
    }

    public void Gamerload() //显示制作人员信息
    {
        SceneManager.LoadScene("gamer");
    }

    public void startload() //返回游戏开始主界面
    {
        SceneManager.LoadScene("start");
    }
}
