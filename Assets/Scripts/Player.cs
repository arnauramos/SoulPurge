using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float initialDmgCounter = 0;

	//  VARIABLES FOR GUNS
	private Rigidbody2D rb2dBullet;
	private GameObject bulletObject;
	private float initialBulletTime;
	private float Counter;

	public int weaponSelected = 0;
	public Transform firePoint;
	private static Weapon[] ArrayWeapon;
	public Weapon weaponUsing;
	private int AuxRounds;
	public int ReservedAmmo;

	//	SEE WEAPON USING VARIABLES
	public int Rounds;
    public int Magazines;
	public int TotalAmmo;

    //VARIABLES FOR OBJECTS
    public GameObject Object;
    public GameObject Key;
    public int keys = 0;

    //VARIABLES FOR DOOR TO SAFE ZONE
    //public GameObject safedoor;
    //private Animator animator;
    //private int openParamID;

    void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;
		AuxSpeed = Speed;

		//	SET WEAPON STATS TO AUXILIARS
		ArrayWeapon = WeaponPlaceholder.ArrayWeapon;

		//	SET WEAPON USING VARIABLES
		Magazines = weaponUsing.Magazines;
		Rounds = AuxRounds;
		TotalAmmo = Magazines * Rounds;

	}

    private void Update()
    {
        //	//	UPDATE WEAPON STATS TO AUXILIARS
        if (health <= 0) Destroy(gameObject);

		// UPDATE WEAPON USING VARIABLES
		weaponUsing = ArrayWeapon[weaponSelected];

		Rounds = weaponUsing.Rounds;
        Magazines = weaponUsing.Magazines;
		AuxRounds = weaponUsing.MaxRounds;


		Reloading();
		GunsSwap();

	}

	private void FixedUpdate()
	{
		fixedDelta = Time.fixedDeltaTime * 1000;
		Counter = Time.time * fixedDelta;
		PlayerMovement();
		PlayerAim();
		if (Counter >= initialBulletTime && Input.GetMouseButton(0))
		{
			if (weaponUsing.Rounds <= 0) return;
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
		if (ReservedAmmo <= 0 && weaponUsing.Magazines <= 0) return;

		if (ReservedAmmo >= weaponUsing.MaxRounds)
		{
			weaponUsing.Magazines += (int)(ReservedAmmo / weaponUsing.MaxRounds);
			ReservedAmmo -= (weaponUsing.MaxRounds * (int)(ReservedAmmo / weaponUsing.MaxRounds));
		}

		if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return)) && weaponUsing.Magazines >= 0 && weaponUsing.Rounds < weaponUsing.MaxRounds)
		{
			Debug.Log("Reloading: Manual;");
			if (weaponUsing.Magazines == 0)
			{
				int AuxRounds2 = weaponUsing.Rounds;
				int AuxReservedAmmo = ReservedAmmo;
				if (weaponUsing.Rounds + ReservedAmmo <= weaponUsing.MaxRounds)
				{
					weaponUsing.Rounds += ReservedAmmo;
					ReservedAmmo = 0;
				}
				else
				{
					ReservedAmmo = (AuxRounds2 + AuxReservedAmmo) - weaponUsing.MaxRounds;
					weaponUsing.Rounds = weaponUsing.MaxRounds;
				}
				return;
			}
			ReservedAmmo += weaponUsing.Rounds;
			weaponUsing.Rounds = AuxRounds;
			weaponUsing.Magazines--;
			return;
		}
        if (weaponUsing.Rounds == 0 && weaponUsing.Magazines > 0)
        {
			Debug.Log("Reloading: Automatic;");
			weaponUsing.Rounds = AuxRounds;
            weaponUsing.Magazines--;
		}
    }

	void GunsSwap()
	{
		string InputKey = Input.inputString;
		if (InputKey == "1" || InputKey == "2" || InputKey == "3")
		{
			if (ArrayWeapon[int.Parse(InputKey) - 1] == null) return;

			Debug.Log("Gun Swap to: " + InputKey);

			switch (InputKey)
			{
				case "1":
					weaponSelected = 0;
					break;
				case "2":
					weaponSelected = 1;
					break;
				case "3":
					weaponSelected = 2;
					break;
				default:
					break;
			}
		}
		else return;
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
				if (collision.gameObject.tag == "Safe_Door" && keys == 3)
				{
						SceneManager.LoadScene("SafeZone");
				}
    }
		private void OnTriggerStay2D(Collider2D collision) //pillar objetos (Albert)
    {
        if (collision.gameObject.tag == "Object" && Input.GetKey(KeyCode.E)) //de momento object solo sera vendas, asi que sumara vida cuando se pille
        {
            health++;
            keys = keys + 3;
            Destroy(Object);
        }
        if (collision.gameObject.tag == "Key_Object" && Input.GetKey(KeyCode.E))
        {
            keys++;
            Destroy(Key);
        }
    }
}
