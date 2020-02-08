using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.eulerAngles = Vector3.forward * 90;
        }
    }
    // Update is called once per frame
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        animator.SetTrigger("Die");
    //        float delaytoload = 1.25f;
    //        Invoke("GoToScene", delaytoload);
    //    }
    //}
}
