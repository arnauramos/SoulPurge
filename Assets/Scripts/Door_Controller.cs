using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Sprite inside;
    public Sprite outside;
    public GameObject Inside;
    public GameObject Object;
    public GameObject Key;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player playerscript = Player.GetComponent<Player>();
    }
    void Update()
    {
        
    }
    //private void close()
    //{
    //    transform.eulerAngles = Vector3.forward * 0;
    //}
    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.eulerAngles = Vector3.forward * 90;
            Inside.gameObject.GetComponent<SpriteRenderer>().sprite = inside;
            Object.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            Key.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (Input.GetKey(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            transform.eulerAngles = Vector3.forward * 0;
            
        }



    }

}
