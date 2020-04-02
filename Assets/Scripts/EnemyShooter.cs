﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShooter : MonoBehaviour
{
    //// VARIABLES FOR AREA OF VISION
    //private Color blue = new Color(0, 0.5f, 1, 0.2f);
    //private Color red = new Color(1, 0, 0, 0.2f);
    //private SpriteRenderer AOVsRenderer;
    //public GameObject AreaOfVision;

    // VARIABLES FOR HEALTH
    public float health = 90f;

    //  VARIABLES FOR GUNS
    public Gun weaponUsing;

    private float fixedDelta;
    private Rigidbody2D rb2dBullet;
    public GameObject bulletObject;
    private float initialBulletTime;
    public float AttackRate = 3f;
    private float Counter;
    public Transform firePoint;


    // VARIABLES FOR MOVEMENT TO PLAYER
    public float Speed = 1000f;
    public Vector2 Direction;
    private Vector3 LookingPlayer = new Vector3(240, 240, 1);
    private Rigidbody2D rb2d;
    private GameObject Player;
    private float rbx, rby;
    private float angle;
    public float LookRange = 6f;
    public float StopRange = 2f;

    // VARIABLES FOR IDLE MOVEMENT
    private Vector3 Idle = new Vector3(150, 150, 1);
    private float lastDirectionChangeTime = 0f;
    private float directionChangeTime = 3.5f;
    private Vector2 movementDirection;

    private DropSouls DropingSoul;
    public GameObject Soul;
    void Start()
    {
   
        rb2d = GetComponent<Rigidbody2D>();
        //AOVsRenderer = AreaOfVision.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");

        // CALCULATE FIRST IDLE MOVEMENT
        NewIdleMovement();

        //CALCULATE NEXT IDLE MOVEMENT
        if (Time.time - lastDirectionChangeTime > directionChangeTime)
        {
            lastDirectionChangeTime = Time.time;
            NewIdleMovement();
        }

        DropingSoul = gameObject.AddComponent<DropSouls>();

        bulletObject = weaponUsing.Bullet;
    }

    private void Update()
    {
        if (health <= 0f)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyDie);
            Destroy(gameObject);
            DropingSoul.DropingSouls(gameObject, Soul);
        }
    }

    private void FixedUpdate()
    {
        fixedDelta = Time.fixedDeltaTime * 1000;

        // GET DISTANCE TO PLAYER
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;

        // MOOVING / IDLE 
        if (Direction.x < LookRange && Direction.x > -LookRange && Direction.y < LookRange && Direction.y > -LookRange)
        {
            LookRange = 8f;
            //AreaOfVision.transform.localScale = LookingPlayer;
            //AOVsRenderer.color = red;
            Movement();

            // SHOOTING
            Counter = Time.time * fixedDelta;
            if (Counter >= initialBulletTime)
            {
                Shooting();
                initialBulletTime = Counter + weaponUsing.FireRate * AttackRate;
            }
        }
        else
        {
            LookRange = 6f;
            //AreaOfVision.transform.localScale = Idle;
            //AOVsRenderer.color = blue;
            IdleMovement();
        }
    }

    void Movement()
    {
        // LOOK AT PLAYER
        angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;

        // MOVE IF FAR FROM PLAYER
        if (Direction.x > StopRange || Direction.x < -StopRange || Direction.y > StopRange || Direction.y < -StopRange)
        {
            rbx = Mathf.Clamp(rb2d.velocity.x, -Speed, Speed);
            rby = Mathf.Clamp(rb2d.velocity.y, -Speed, Speed);
            rb2d.velocity = new Vector2(rbx, rby);
            rb2d.AddForce(transform.up * Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

    }
    void NewIdleMovement()
    {
        //CHOOSE A NEW DIRECTION AND ROTATE
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        angle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg - 90f;
    }
    void IdleMovement()
    {
        // IDLE MOVE AND STOP 0.5 SECS BEFORE NEXT DIRECTION CHANGE
        if (Time.time - lastDirectionChangeTime < directionChangeTime - 0.5f)
        {
            rb2d.rotation = angle;
            rb2d.AddForce(transform.up * Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
    void Shooting()
    {
        bulletObject = Instantiate(weaponUsing.Bullet, firePoint.position, firePoint.rotation);
        rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
        rb2dBullet.AddForce(firePoint.up * weaponUsing.BulletSpeed, ForceMode2D.Impulse);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyShooting);
        Destroy(bulletObject, weaponUsing.Range * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().PlayerShoot)
        {
            health -= PlayerManager.Instance.PlayerGunList[PlayerManager.Instance.weaponSelected].Damage;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyDamage);
            return;
        }
    }
}

