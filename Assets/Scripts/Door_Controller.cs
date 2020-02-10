using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;
    //public bool dooropened;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //dooropened = false;
        
    }
    void Update()
    {
     
    }
    private void close()
    {
        transform.eulerAngles = Vector3.forward * 0;
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.eulerAngles = Vector3.forward * 90;
            Invoke("close", 5.0f);
            //inside.transform.position = new Vector3(inside.transform.position.x, inside.transform.position.y, 0);
            //dooropened = true;
        }
        //if (collision.gameObject.tag == "Player" && dooropened == true)
        //{
        //    transform.eulerAngles = Vector3.forward * 0;
        //    dooropened = false;
        //}
    }
}
