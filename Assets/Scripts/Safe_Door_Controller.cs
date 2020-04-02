using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Door_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public GameObject player;
    public int player_keys;
    private Animator animator;
    private int openParamID;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        Player p = player.GetComponent<Player>();
        openParamID = Animator.StringToHash("Opening");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (PlayerManager.Instance.keys >= 3 && collision.gameObject.tag == "Player")
        {

            animator.SetBool("Opening", true);
            Destroy(this.gameObject);
        }

    }
}
