using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // VARIABLES FOR AREA OF VISION
    private Color blue = new Color(0, 0.5f, 1, 0.2f);
    private Color red = new Color(1, 0, 0, 0.2f);
    public GameObject AreaOfVision;
    private SpriteRenderer AOVsRenderer;

    // VARIABLES FOR HEALTH
    public float health = 150f;
    private Player PlayerScript;

    // VARIABLES FOR ATTACK
    public float dmg = 10f;
    public float AttackRate = 10f;

    // VARIABLES FOR MOVEMENT TO PLAYER
    private GameObject Player;
    public float Speed = 1000f;
    public Vector2 Direction;
    private Vector3 LookingPlayer = new Vector3(120, 120, 1);
    private Rigidbody2D rb2d;
    private float rbx, rby;
    private float angle;
    public float LookRange = 1.5f;

    // VARIABLES FOR IDLE MOVEMENT
    private Vector3 Idle = new Vector3(75, 75, 1);
    private float lastDirectionChangeTime = 0f;
    private float directionChangeTime = 3.5f;
    private Vector2 movementDirection;

    private DropSouls DropingSoul;
    public GameObject Soul;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        AOVsRenderer = AreaOfVision.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");

        // CALCULATE FIRST IDLE MOVEMENT
        NewIdleMovement();

        // GET PLAYER SCRIPT TO KNOW HIS WEAPON
        PlayerScript = Player.GetComponent<Player>();

        DropingSoul = gameObject.AddComponent<DropSouls>();
    }

    private void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
            DropingSoul.DropingSouls(gameObject, Soul);
        }
    }


    private void FixedUpdate()
    {
        // GET DISTANCE TO PLAYER
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;

        // MOOVING / IDLE
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
            IdleMovement();
        }

        //CALCULATE NEXT IDLE MOVEMENT
        if (Time.time - lastDirectionChangeTime > directionChangeTime)
        {
            lastDirectionChangeTime = Time.time;
            NewIdleMovement();
        }
    }

    void Movement()
    {
        // ROTATE LOOKING AT PLAYER
        angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;

        // SET A MAX SPEED
        rbx = Mathf.Clamp(rb2d.velocity.x, -Speed, Speed);
        rby = Mathf.Clamp(rb2d.velocity.y, -Speed, Speed);
        rb2d.velocity = new Vector2(rbx, rby);

        // MOVE THE ENEMY TOWARDS THE PLAYER
        rb2d.AddForce(transform.up * Speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().PlayerShoot)
        {
            health -= PlayerScript.weaponUsing.Damage;
        }
    }
}
