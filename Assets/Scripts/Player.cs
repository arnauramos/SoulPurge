using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 mousePosition;
    private Vector2 Movement;
    private Rigidbody2D rb2d;
    private Rigidbody2D rb2dBullet;
    public GameObject Bullet;
    private GameObject bulletObject;
    public Transform firePoint;
    private Vector2 BulletSpeed;
    public float initialBulletTime;
    private float deltaBulletTime;
    public int weaponSelected;
    public GameObject[] Weapon;
    private GameObject weaponUsing;
    //private WeaponPlaceholder weaponPlaceholder;
    //private float delta;
    private float fixedDelta;
    public float Speed;
    public float Counter;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Movement = Vector2.zero;
        Speed = 5f;
        BulletSpeed = new Vector2(10, 10);
        initialBulletTime = 0;
        deltaBulletTime = 3f;
        weaponUsing = Weapon[weaponSelected];
        //weaponPlaceholder = weaponUsing.GetComponent<WeaponPlaceholder>();
    }
    /*
    private void FixedUpdate()
    {
        fixedDelta = Time.fixedDeltaTime * 1000;
        Counter = Time.time * fixedDelta;
        PlayerMovement();
        PlayerAim();
        if (Counter >= initialBulletTime && Input.GetMouseButton(0))
        {  
            Shooting();
            initialBulletTime = Counter + deltaBulletTime;
        }

    }
    */
    void Update()
    {
        //delta = Time.deltaTime * 1000;     
    }

    void PlayerMovement()
    {
        Movement.x = Input.GetAxis("Horizontal");
        Movement.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift)) Speed = 10f;
        else Speed = 5f; 
        rb2d.velocity = Movement * Speed * fixedDelta;
    }
    void PlayerAim()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 lookDirection = mousePosition - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }
    void Shooting()
    {
        bulletObject = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
        rb2dBullet.velocity = BulletSpeed * firePoint.up * fixedDelta;
        Destroy(bulletObject,2f /*weaponPlaceholder.weaponRange*/);
    }
}
