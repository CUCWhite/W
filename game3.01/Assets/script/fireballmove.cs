using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballmove : MonoBehaviour {

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(new Vector3(Speed* 0.1f, 0f, 0f));
        if (transform.position.x > 12f|| transform.position.x < -15f)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ground")
           Destroy(this.gameObject);
    }
}
