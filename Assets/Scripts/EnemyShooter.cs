using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float Speed = 500f;
    public Vector2 Direction;
    private Vector3 LookingPlayer = new Vector3(240, 240, 1);
    private Vector3 Idle = new Vector3(150, 150, 1);
    private Color blue = new Color(0, 0.5f, 1, 0.2f);
    private Color red = new Color(1, 0, 0, 0.2f);
    private Rigidbody2D rb2d;
    private SpriteRenderer AOVsRenderer;
    private GameObject Player;
    private float rbx, rby;
    private float angle;
    public float LookRange = 3.2f;
    public float dmg = 1f;
    public float AttackRate = 10f;
    public GameObject AreaOfVision;

    //  VARIABLES FOR GUNS
    private float fixedDelta;
    private Rigidbody2D rb2dBullet;
    private GameObject bulletObject;
    private float initialBulletTime;
    private float Counter;

    public int weaponSelected = 0;
    public Transform firePoint;
    private static Weapon[] ArrayWeapon;
    public Weapon weaponUsing;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        AOVsRenderer = AreaOfVision.GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");

        //	SET WEAPON STATS TO AUXILIARS
        ArrayWeapon = WeaponPlaceholder.ArrayWeapon;
        weaponUsing = ArrayWeapon[weaponSelected];
    }

    private void FixedUpdate()
    {
        fixedDelta = Time.fixedDeltaTime * 1000;
        Direction.x = Player.transform.position.x - transform.position.x;
        Direction.y = Player.transform.position.y - transform.position.y;
        if (Direction.x < LookRange && Direction.x > -LookRange && Direction.y < LookRange && Direction.y > -LookRange)
        {
            LookRange = 3.2f;
            AreaOfVision.transform.localScale = LookingPlayer;
            AOVsRenderer.color = red;
            Movement();
        }
        else
        {
            LookRange = 2;
            AreaOfVision.transform.localScale = Idle;
            AOVsRenderer.color = blue;
        }
        Counter = Time.time * fixedDelta;
        if (Counter >= initialBulletTime)
        {
            Shooting();
            initialBulletTime = Counter + weaponUsing.FireRate*3;
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
    void Shooting()
    {
        bulletObject = Instantiate(weaponUsing.Bullet, firePoint.position, firePoint.rotation);
        rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
        rb2dBullet.AddForce(firePoint.up * weaponUsing.BulletSpeed, ForceMode2D.Impulse);
        Destroy(bulletObject, weaponUsing.Range);
    }
}
