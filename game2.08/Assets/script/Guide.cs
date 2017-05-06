using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Guide : MonoBehaviour
{

    public GameObject Player;
    private GameObject Player_clone;
    private Animator anim;                           //player的动画机
    public Text click;
    public Image mainmask;
    public Text subtitles;
    float init_sy;
    int hang_count;
    // Use this for initialization
    void Start()
    {
        init_sy = subtitles.transform.position.y;
        hang_count = 0;
        Invoke("Timer", 2.0f);
        Instantiate(Player, new Vector3(0f, -1.16f, 0f), Quaternion.identity);
        Player_clone = GameObject.Find("White(Clone)");

        anim = Player_clone.GetComponent<Animator>();
        click.color = UnityEngine.Color.clear;
    }

    // Update is called once per frame
    void Update()
    {

        mainmask.DOColor(UnityEngine.Color.clear, 5.0f);
        click.DOBlendableColor(UnityEngine.Color.white, 3.0f);
        anim.SetFloat("Speed", 2.0f);
        if (Input.GetMouseButton(0))
            SceneManager.LoadScene("level1");

    }
    void Timer()
    {
        if (hang_count < 15)
        {
            hang_count++;
            float sy = subtitles.transform.position.y;
            subtitles.transform.DOMoveY(sy + 47, 1.0f, true);
        }
        else
        {
            hang_count = 0;
            subtitles.transform.DOMoveY(init_sy, 1.0f, true);
        }
        Invoke("Timer", 2.0f);
    }
}
