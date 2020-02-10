using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Change : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Sprite inside;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = inside;
        }
    }
}
