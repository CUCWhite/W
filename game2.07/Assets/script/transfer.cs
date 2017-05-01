using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transfer : MonoBehaviour
{
    public GameObject another_transfer;
    public bool on_off;
    public bool empty;
    float Extents;

    // Use this for initialization
    void Start()
    {
        Extents = 1;
    }

    void Update()
    {
        SearchAllUnits();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(on_off)
        {
            if (other.tag != "ground" && other.tag != "Player")
            {
                other.transform.position = another_transfer.transform.position;
                another_transfer.GetComponent<transfer>().on_off = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (on_off)
        {
            if (other.tag == "Player")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    other.transform.position = another_transfer.transform.position;
                    another_transfer.GetComponent<transfer>().on_off = false;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!on_off)
        {
            if (other.tag != "ground")
            {
                if (empty)
                    on_off = true;
            }
        }
    }

    void SearchAllUnits() //搜索某范围内的碰撞盒，用于判定player左右是否靠着一个物体
    {
        Collider2D[] collidl = Physics2D.OverlapCircleAll(transform.position, Extents, LayerMask.NameToLayer("Activity"));

        if (collidl.Length <= 0)
        {
            empty = true;
        }
        else empty = false;
    }
}
