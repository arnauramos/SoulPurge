using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Sprite inside;
    public Sprite outside;
    public GameObject Inside;
    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleChange = new Vector3(-0.42f, -0.42f,-1);
    }
    void Update()
    {
     
    }
    //private void close()
    //{
    //    transform.eulerAngles = Vector3.forward * 0;
    //}
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.eulerAngles = Vector3.forward * 90;
            Inside.gameObject.GetComponent<SpriteRenderer>().sprite = inside;
            Inside.transform.localScale += scaleChange;

        }
        if (Input.GetKey(KeyCode.E) && collision.gameObject.tag == "Player")
        {
            transform.eulerAngles = Vector3.forward * 0;
            
        }



    }

}
