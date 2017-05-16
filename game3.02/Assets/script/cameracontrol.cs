using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameracontrol : MonoBehaviour {
    public GameObject player;
    private float edge_left;
    private float edge_right;
    private float edge_up;
    private float edge_down;

    public int level;
    private int state;

    private float PosX;
    private float PosY;
    private float DistanceX;
    private float DistanceY;

    private float hor;
    private float ver;


    // Use this for initialization
    void Start () {
        state = 0;
        DistanceX = 5.5f;
        DistanceY = 2.5f;
        PosX = transform.position.x;
        PosY = transform.position.y;
        hor = ver = 0;

        switch (level)
        {
            case 3:
                edge_left = 0f;
                edge_right = 0f;
                edge_up = 0f;
                edge_down = -27f;
                break;
            case 2:
                edge_left = 0f;
                edge_right = 5.55f;
                edge_up = 2.8f;
                edge_down = 0f;
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player == null)
        {
            player = GameObject.Find("White(Clone)");
        }

        if (state == 0)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) >= DistanceX || Mathf.Abs(player.transform.position.y - transform.position.y) >= DistanceY)
            {
                SetPos(player.transform.position.x, player.transform.position.y);
            }
        }
        
        LookAround();
        LookBack();

        hor = Input.GetAxis("Cameractrl_Hor");
        ver = Input.GetAxis("Cameractrl_Ver");

    }

    void LookAround()
    {
        if (hor<0)
        {
            state++;
            transform.DOMove(new Vector3(edge_left, transform.position.y, transform.position.z), 1f);
            PosX = edge_left;
        }
        if (hor>0)
        {
            state++;
            transform.DOMove(new Vector3(edge_right, transform.position.y, transform.position.z), 1f);
            PosX = edge_right;
        }
        if (ver<0)
        {
            state++;
            transform.DOMove(new Vector3(transform.position.x, edge_up, transform.position.z), 1f);
            PosY = edge_up;
        }
        if (ver>0)
        {
            state++;
            transform.DOMove(new Vector3(transform.position.x, edge_down, transform.position.z), 1f);
            PosY = edge_down;
        }
    }

    void LookBack()
    {
        if (hor==0)
        {
            state--;
            PosX = player.transform.position.x;
            if (PosX > edge_right)
                PosX = edge_right;
            else if (PosX < edge_left)
                PosX = edge_left;
            transform.DOMove(new Vector3(PosX, PosY, transform.position.z), 1f);
        }
        if (ver==0)
        {
            state--;
            PosY = player.transform.position.y;
            if (PosY < edge_down)
                PosY = edge_down;
            else if (PosY > edge_up)
                PosY = edge_up;
            transform.DOMove(new Vector3(PosX, PosY, transform.position.z), 1f);
        }
    }

    void SetPos(float PosX, float PosY)
    {
        if (PosX > edge_right)
            PosX = edge_right;
        else if (PosX < edge_left)
            PosX = edge_left;

        if (PosY > edge_up)
            PosY = edge_up;
        else if (PosY < edge_down)
            PosY = edge_down;
        
        transform.DOMove(new Vector3(PosX, PosY, transform.position.z), 1f);
    }
}
