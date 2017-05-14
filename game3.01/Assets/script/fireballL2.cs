using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballL2 : MonoBehaviour
{

    private GameObject player;
    private GameObject monsterfly;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("White(Clone)");
        monsterfly = GameObject.Find("怪物2飞行_0");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.Find("White(Clone)");
        if (monsterfly == null)
            monsterfly = GameObject.Find("怪物2飞行_0");

        transform.Translate(new Vector3((player.transform.position.x - transform.position.x)*0.1f, (player.transform.position.y - transform.position.y)*0.1f, 0f));

    }
}
