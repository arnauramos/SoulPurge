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

	//  VARIABLES FOR GUNS
	private Rigidbody2D rb2dBullet;
	private GameObject bulletObject;
	private float initialBulletTime;
	private float Counter;

	public int weaponSelected = 0;
	public Transform firePoint;
	private static Weapon[] ArrayWeapon;
	public Weapon weaponUsing;

    //VARIABLES FOR OBJECTS
    public GameObject Object;
    public GameObject Key;
    public int health = 5;
    public int keys = 0;

    void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;
		AuxSpeed = Speed;

		//	SET WEAPON STATS TO AUXILIARS
		ArrayWeapon = WeaponPlaceholder.ArrayWeapon;
		weaponUsing = ArrayWeapon[weaponSelected];
	}

	//private void Update()
	//{
	//	//	SET WEAPON STATS TO AUXILIARS
	//}

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
		if (Input.GetKey(KeyCode.LeftShift)) Speed = Sprint;
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
	}
    private void OnTriggerEnter2D(Collider2D collision) //pillar objetos (Albert)
    {
        if (collision.gameObject.tag == "Object") //de momento object solo sera vendas, asi que sumara vida cuando se pille
        {
            health++;
            Destroy(Object);
        }

        if (collision.gameObject.tag == "Key_Object") 
        {
            keys++;
            Destroy(Key);
        }


    }
}
