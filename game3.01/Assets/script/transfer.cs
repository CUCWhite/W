using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transfer : MonoBehaviour
{
    public GameObject another_transfer;
    public Vector3 transfer_pos;
    public bool on_off;
    //public bool empty;
    float Extents;

    // Use this for initialization
    void Start()
    {
        Extents = 1;
    }

    void FixedUpdate()
    {
        SearchAllUnits();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        if (on_off)
        {
            Debug.Log("2");
            if (other.tag != "ground"||other.tag=="fireball")
            {
                other.transform.position = transfer_pos;
                another_transfer.GetComponent<transfer>().on_off = false;
                Debug.Log("3");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!on_off)
        {
            if (other.tag != "ground")
            {
                //if (empty)
                    on_off = true;
                Debug.Log("4");
            }
        }
    }

    void SearchAllUnits() //搜索某范围内的碰撞盒，用于判定传送门区域中是否有枯树
    {
        Collider2D[] collidl = Physics2D.OverlapCircleAll(transform.position, Extents, 1<<LayerMask.NameToLayer("Tree"));

        for (int i = 0; i < collidl.Length; i++)
            if (collidl[i].tag == "tree")
            {
                another_transfer.GetComponent<transfer>().on_off = false;
            }
            else{ }
    }
}
