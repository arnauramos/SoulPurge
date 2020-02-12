using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit_door : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Background")
        {
            door.transform.eulerAngles = Vector3.forward * 0;
        }
    }
}
