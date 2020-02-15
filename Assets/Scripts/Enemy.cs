using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 500f;
    public Vector2 Direction;
    private Vector3 LookingPlayer = new Vector3(120, 120, 1);
    private Vector3 Idle = new Vector3(75,75,1);
    private Color blue = new Color(0, 0.5f, 1, 0.2f);
    private Color red = new Color(1, 0, 0, 0.2f);
    private Rigidbody2D rb2d;
    private SpriteRenderer AOVsRenderer;
    private GameObject Player;
    private float rbx, rby;
    private float angle;
    public float LookRange = 1.5f;
    public float dmg = 1f;
    public float AttackRate = 10f;
    public GameObject AreaOfVision;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        AOVsRenderer = AreaOfVision.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;
        if (Direction.x < LookRange && Direction.x > -LookRange && Direction.y < LookRange && Direction.y > -LookRange)
        {
            LookRange = 2.5f;
            AreaOfVision.transform.localScale = LookingPlayer;
            AOVsRenderer.color = red;
            Movement();
        }
        else
        {
            LookRange = 1.5f;
            AreaOfVision.transform.localScale = Idle;
            AOVsRenderer.color = blue;
        }
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
