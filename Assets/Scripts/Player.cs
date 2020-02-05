using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector2 mousePosition;
	private Vector2 Movement;
	private Rigidbody2D rb2d;
	private Rigidbody2D rb2dBullet;

	//private float delta;
	private float fixedDelta;
	public float Speed = 0.1f;
	private float AuxSpeed;
	public float Sprint = 0.2f;

	//  VARIABLES FOR GUNS -- DEPRECATED
	private GameObject bulletObject;
	//public Transform firePoint;
	//private Vector2 BulletSpeed;
	public float initialBulletTime;
	//private float deltaBulletTime;
	public float Counter;

	// VARAIBLES FOR GUNS
	GameObject WeaponPlaceholderClass;
	public int weaponSelected = 0;
	private static Weapon[] ArrayWeapon;
	private Weapon weaponUsing;
	//private WeaponArray_Test weaponUsing;

	private void Awake()
	{
	}
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;
		AuxSpeed = Speed;
		//initialBulletTime = 0;
		//deltaBulletTime = 3f;

		//	SET WEAPON STATS TO AUXILIARS
		ArrayWeapon = WeaponPlaceholder.ArrayWeapon;

		//WeaponPlaceholderClass = GameObject.Find("WeaponPlaceholder");
		//ArrayWeapon = GameObject.Find("WeaponPlaceholder").GetComponent<WeaponPlaceholder>().ArrWeapon;
		weaponSelected = 1;
		weaponUsing = ArrayWeapon[weaponSelected];
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

	void Update()
	{
		//delta = Time.deltaTime * 1000;     
	}

	void PlayerMovement()
	{
		Movement.x = Input.GetAxis("Horizontal");
		Movement.y = Input.GetAxis("Vertical");
		if (Input.GetKey(KeyCode.LeftShift)) Speed = Sprint;
		else Speed = AuxSpeed; 
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
		bulletObject = Instantiate(weaponUsing.Bullet, weaponUsing.FirePoint.position,Quaternion.identity);
		rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
		rb2dBullet.velocity = weaponUsing.BulletSpeed * weaponUsing.FirePoint.up * fixedDelta;
		Destroy(bulletObject, weaponUsing.FireRate);
	}
}
