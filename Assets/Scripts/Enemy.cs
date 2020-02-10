using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;
    public Vector2 Direction;
    private Rigidbody2D rb2d;
    private GameObject Player;
    private float rbx, rby;
    private float angle;
    public float LookRange = 30f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;
        if (Direction.x < LookRange && Direction.x > -LookRange && Direction.y < LookRange && Direction.y > -LookRange) { Movement(); }
    }

    void Movement()
    {
        angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;

    
        rbx = Mathf.Clamp(rb2d.velocity.x, -Speed, Speed);
        rby = Mathf.Clamp(rb2d.velocity.y, -Speed, Speed);

        rb2d.velocity = new Vector2(rbx, rby);
        rb2d.AddForce(transform.up * Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    angle = 0f;
    //}
}
