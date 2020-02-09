using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;
    public bool dooropened;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dooropened = false;
        
    }
    void Update()
    {
     
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            transform.eulerAngles = Vector3.forward * 90;
            dooropened = true;

        }
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E) && dooropened == true)
        {
            transform.eulerAngles = Vector3.forward * 0;
            dooropened = false;
        }
    }
}
