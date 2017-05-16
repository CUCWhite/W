using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballborn : MonoBehaviour {

    public GameObject fireballs;
    public float fx, fy;

	// Use this for initialization
	void Start () {
        Invoke("Timer", 3.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Timer()
    {
        Instantiate(fireballs, new Vector3(fx,fy, 0f), Quaternion.identity);
        Invoke("Timer", 2.0f);
    }
}
