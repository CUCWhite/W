using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballmove : MonoBehaviour {

    public float Speed;
    bool changespeed;

	// Use this for initialization
	void Start () {
        changespeed = true;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(new Vector3(Speed* 0.1f, 0f, 0f));
        if (transform.position.x > 12f|| transform.position.x < -15f)
            Destroy(this.gameObject);
        if (transform.position.y < -20f && changespeed)
        {
            changespeed = false;
            Speed = -Speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ground")
           Destroy(this.gameObject);
    }
}
