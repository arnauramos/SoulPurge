using System.Collections;
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
    public float LookRange = 2000f;
    public float StopRange = 2f;

    // BULLET PUSH
    private Vector3 pushDirection;
    private bool hit = false;
    private float hitTimer = 0f;

    // VARIABLES FOR IDLE MOVEMENT
    private Vector3 Idle = new Vector3(150, 150, 1);
    private float lastDirectionChangeTime = 0f;
    private float directionChangeTime = 3.5f;
    private Vector2 movementDirection;

    private DropSouls DropingSoul;
    public GameObject Soul;

    // VARIABLES FOR ANIMATIONS
    public GameObject Graphic;
    public GameObject Feet;
    private Animator feetAnimator;
    private SpriteRenderer GraphicSprite;
    private SpriteRenderer FeetSprite;
    private int walkingParamID;

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

        // GET ANIMATOR COMPONENTS
        feetAnimator = Feet.GetComponent<Animator>();
        walkingParamID = Animator.StringToHash("Walking");

        GraphicSprite = Graphic.GetComponent<SpriteRenderer>();
        FeetSprite = Feet.GetComponent<SpriteRenderer>();

        //CHANGE COLOR
        ChangeColor();
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

            // SHOOTING
            Counter = Time.time * fixedDelta;
            Direction.x = Mathf.Abs(Direction.x);
            Direction.y = Mathf.Abs(Direction.y);
            if (Counter >= initialBulletTime && (Direction.x <= 10.8f && Direction.y <= 3.8f))
            {
                Shooting();
                initialBulletTime = Counter + weaponUsing.FireRate * AttackRate;
            }
        }
        else
        {
            LookRange = 2000f;
            //AreaOfVision.transform.localScale = Idle;
            //AOVsRenderer.color = blue;
            IdleMovement();
        }

        CheckMovement();
    }

    void ChangeColor()
    {
        // SET COLORS
        // RED: 127 - 255 & >= GREEN
        // GREEN: 0 - 255
        // BLUE: GREEN
        int green = Random.Range(0, 256);
        int blue = green;
        int red;
        do
        {
            red = Random.Range(127, 256);
        } while (red < green);

        // APPLY COLORS
        Color32 newColor = new Color32((byte)red, (byte)green, (byte)blue, 255);
        GraphicSprite.color = newColor;
        FeetSprite.color = newColor;

        // CHANGE STATS
        //> RED == < HP > SPEED
        //< RED == > HP < SPEED

        // Speed: max 256 min -128 (906 to 618)
        // Health: min -25 max 51 (65 to 141)
        if (red >= 191)
        {
            Speed += (red - 191) * 4;
            health -= (red - 191) * 0.4f;
        }
        else
        {
            Speed -= (191 - red) * 2;
            health += (191 - red) * 0.8f;
        }
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
        rb2dBullet.AddForce(firePoint.up * (weaponUsing.BulletSpeed * 0.7f), ForceMode2D.Impulse);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyShooting);
        Destroy(bulletObject, weaponUsing.Range * 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().PlayerShoot)
        {
            hit = true;
            pushDirection = collision.transform.up;
            health -= PlayerManager.Instance.PlayerGunList[PlayerManager.Instance.weaponSelected].Damage;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.EnemyDamage);
            return;
        }
    }
}

