using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;
    private Vector2 Direction;
    private Rigidbody2D rb2d;
    private GameObject Player;
    float rbx, rby;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;
        rb2d.AddForce(Direction.normalized * Speed * Time.fixedDeltaTime,ForceMode2D.Impulse);

        rbx = Mathf.Clamp(rb2d.velocity.x, -Speed, Speed);
        rby = Mathf.Clamp(rb2d.velocity.y, -Speed, Speed);

        rb2d.velocity = new Vector2(rbx, rby); 

    }
}
