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

    // Use this for initialization
    void Start () {
        state = 0;
        PosX = transform.position.x;
        PosY = transform.position.y;

        switch (level)
        {
            case 3:
                edge_left = 0f;
                edge_right = 0f;
                edge_up = 0f;
                edge_down = -6f;
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
            SetPos(player.transform.position.x, player.transform.position.y);
        }
        
        LookAround();
        LookBack();
    }

    void LookAround()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            state++;
            transform.DOMove(new Vector3(edge_left, transform.position.y, transform.position.z), 1f);
            PosX = edge_left;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            state++;
            transform.DOMove(new Vector3(edge_right, transform.position.y, transform.position.z), 1f);
            PosX = edge_right;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            state++;
            transform.DOMove(new Vector3(transform.position.x, edge_up, transform.position.z), 1f);
            PosY = edge_up;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            state++;
            transform.DOMove(new Vector3(transform.position.x, edge_down, transform.position.z), 1f);
            PosY = edge_down;
        }
    }

    void LookBack()
    {
        if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.L))
        {
            state--;
            PosX = player.transform.position.x;
            if (PosX > edge_right)
                PosX = edge_right;
            else if (PosX < edge_left)
                PosX = edge_left;
            transform.DOMove(new Vector3(PosX, PosY, transform.position.z), 1f);
        }
        if (Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.K))
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

        transform.position = new Vector3(PosX, PosY, transform.position.z);
    }
}
