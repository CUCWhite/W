using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kutreeburn : MonoBehaviour {

    bool isBurn;          //枯树是否已经被烧掉

	// Use this for initialization
	void Start () {
        isBurn = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "fireball")
        {
            isBurn = true;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
