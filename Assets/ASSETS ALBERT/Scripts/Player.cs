using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector2 mousePosition;
	private Vector2 Movement;
	private Rigidbody2D rb2d;

	//private float delta;
	private float fixedDelta;
	public float Speed = 0.1f;
	private float AuxSpeed;
	public float Sprint = 0.2f;

    public float health = 100f;
    public float dmgCounter = 100f;
    private float initialDmgCounter;

	//  VARIABLES FOR GUNS
	private Rigidbody2D rb2dBullet;
	private GameObject bulletObject;
	private float initialBulletTime;
	private float Counter;

	public int weaponSelected = 0;
	public Transform firePoint;
	private static Weapon[] ArrayWeapon;
	public Weapon weaponUsing;

    public int Rounds;
    private int AuxRounds;
    public int Magazines;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;
		AuxSpeed = Speed;

		//	SET WEAPON STATS TO AUXILIARS
		ArrayWeapon = WeaponPlaceholder.ArrayWeapon;
		weaponUsing = ArrayWeapon[weaponSelected];
        AuxRounds = weaponUsing.Rounds;
        Rounds = AuxRounds;
	}

    private void Update()
    {
        //	//	SET WEAPON STATS TO AUXILIARS
        if (health <= 0) Destroy(gameObject);
        Rounds = weaponUsing.Rounds;
        Magazines = weaponUsing.Magazines;
        Reloading();
    }

	private void FixedUpdate()
	{
		fixedDelta = Time.fixedDeltaTime * 1000;
		Counter = Time.time * fixedDelta;
		PlayerMovement();
		PlayerAim();
		if (Counter >= initialBulletTime && Input.GetMouseButton(0))
		{
			Shooting();
			initialBulletTime = Counter + weaponUsing.FireRate;
		}
	}

	void PlayerMovement()
	{
		Movement.x = Input.GetAxis("Horizontal");
		Movement.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.Keypad0))) Speed = Sprint;
        else Speed = AuxSpeed;

		if (rb2d.velocity.x > Speed || rb2d.velocity.x < Speed) rb2d.velocity = new Vector2 (0, 0);
		rb2d.AddForce(Movement * Speed * fixedDelta, ForceMode2D.Impulse);
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
		bulletObject = Instantiate(weaponUsing.Bullet, firePoint.position, firePoint.rotation);
		rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
		rb2dBullet.AddForce(firePoint.up * weaponUsing.BulletSpeed, ForceMode2D.Impulse);
		Destroy(bulletObject, weaponUsing.Range);
        weaponUsing.Rounds--;
	}
    void Reloading()
    {
        if (Rounds <= 0 && Magazines > 0)
        {
            Rounds = AuxRounds;
            Magazines--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            dmgCounter = Time.time * fixedDelta;
            if (dmgCounter >= initialDmgCounter)
            {
                health -= collision.gameObject.GetComponent<Enemy>().dmg;
                initialDmgCounter = dmgCounter + collision.gameObject.GetComponent<Enemy>().AttackRate;
            }
        }
    }
}
