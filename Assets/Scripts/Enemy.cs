using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //// VARIABLES FOR AREA OF VISION
    //private Color blue = new Color(0, 0.5f, 1, 0.2f);
    //private Color red = new Color(1, 0, 0, 0.2f);
    //public GameObject AreaOfVision;
    //private SpriteRenderer AOVsRenderer;

    // VARIABLES FOR HEALTH
    public float health = 150f;

    // VARIABLES FOR ATTACK
    public float dmg = 10f;
    public float AttackRate = 10f;

    // VARIABLES FOR MOVEMENT TO PLAYER
    private GameObject Player;
    public float Speed = 2000f;
    public Vector2 Direction;
    private Vector3 LookingPlayer = new Vector3(120, 120, 1);
    private Rigidbody2D rb2d;
    private float rbx, rby;
    private float angle;
    public float LookRange = 2000f;

    // BULLET PUSH
    private Vector3 pushDirection;
    private bool hit = false;
    private float hitTimer = 0f;

    // VARIABLES FOR IDLE MOVEMENT
    private Vector3 Idle = new Vector3(75, 75, 1);
    private float lastDirectionChangeTime = 0f;
    private float directionChangeTime = 3.5f;
    private Vector2 movementDirection;

    private DropSouls DropingSoul;
    public GameObject Soul;

    // VARIABLES FOR ANIMATIONS
    public GameObject Feet;
    private Animator feetAnimator;
    private int walkingParamID;

    //private Component ArrTest;
    //private MonoBehaviour Test;

    //private GameObject Gol;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //AOVsRenderer = AreaOfVision.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");

        // CALCULATE FIRST IDLE MOVEMENT
        NewIdleMovement();

        DropingSoul = gameObject.AddComponent<DropSouls>();

        // GET ANIMATOR COMPONENTS
        feetAnimator = Feet.GetComponent<Animator>();
        walkingParamID = Animator.StringToHash("Walking");
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
        if (hit)
        {
            hitTimer += Time.fixedDeltaTime;
            rb2d.AddForce(pushDirection * Speed * 2 * Time.fixedDeltaTime, ForceMode2D.Impulse);
            if (hitTimer >= 0.035f)
            {
                hitTimer = 0f;
                hit = false;
            }
            return;
        }
        // GET DISTANCE TO PLAYER
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;

        // MOOVING / IDLE
        if (Direction.x < LookRange && Direction.x > -LookRange && Direction.y < LookRange && Direction.y > -LookRange)
        {
            LookRange = 2000f;
            //AreaOfVision.transform.localScale = LookingPlayer;
            //AOVsRenderer.color = red;
            Movement();
        }
        else
        {
            LookRange = 2000f;
            //AreaOfVision.transform.localScale = Idle;
            //AOVsRenderer.color = blue;
            IdleMovement();
        }

        //CALCULATE NEXT IDLE MOVEMENT
        if (Time.time - lastDirectionChangeTime > directionChangeTime)
        {
            lastDirectionChangeTime = Time.time;
            NewIdleMovement();
        }

        CheckMovement();
    }
    private void CheckMovement()
    {
        if (rb2d.velocity.y >= 0.5f || rb2d.velocity.x >= 0.5f || rb2d.velocity.y <= -0.5f || rb2d.velocity.x <= -0.5f)
        {
            feetAnimator.SetBool(walkingParamID, true);
        }
        else
        {
            feetAnimator.SetBool(walkingParamID, false);
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
            hit = true;
            pushDirection = collision.transform.up;
            health -= PlayerManager.Instance.PlayerGunList[PlayerManager.Instance.weaponSelected].Damage;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyDamage);
        }
    }
}
