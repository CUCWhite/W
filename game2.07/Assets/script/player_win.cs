using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class player_win : MonoBehaviour {

    public float pl_win_x;              //过关时player的x位置
    public float pl_win_y;              //过关时player的y位置
    public GameObject main_mask;
    // Use this for initialization
    void Awake()
    {
        transform.position = new Vector3(pl_win_x, pl_win_y, 0);
        Instantiate(main_mask, transform.position, Quaternion.identity);
        main_mask.transform.DOScale(10.0f, 0.0f);
    }
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Time.deltaTime,0, 0, Space.World);
        /*
         if (transform.position.x > 9.5)
         {
             Destroy(this.gameObject);
             Debug.Log("you win");
         }*/

        if (main_mask)
        {
            main_mask.transform.DOScale(5.0f, 1.0f);
            main_mask.GetComponent<SpriteRenderer>().DOColor(UnityEngine.Color.black, 1.0f);
            main_mask.transform.DOMove(transform.position, 1.0f);

        }
        Camera.main.transform.DOMoveX(transform.position.x, 3.0f);
        Camera.main.transform.DOMoveY(transform.position.y, 3.0f);

    }
}
