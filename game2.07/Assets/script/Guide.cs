using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Guide : MonoBehaviour {
    public GameObject Player;
    private GameObject Player_clone;
    private Animator anim;                           //player的动画机
    public Text click;
    public Image mainmask;
    // Use this for initialization
    void Start () {
        Instantiate(Player, new Vector3(0f, -1.16f, 0f), Quaternion.identity);
        Player_clone = GameObject.Find("White(Clone)");

        anim = Player_clone.GetComponent<Animator>();
        click.color = UnityEngine.Color.clear;
    }
	
	// Update is called once per frame
	void Update () {
        mainmask.DOColor(UnityEngine.Color.clear, 5.0f);
        click.DOBlendableColor(UnityEngine.Color.white, 3.0f);
        anim.SetFloat("Speed", 2.0f);
        if(Input.GetMouseButton(0))
            SceneManager.LoadScene("level1");
    }
}
