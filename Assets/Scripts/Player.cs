using System;
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
	
	[Header("Variables for player:")]
	[Space(5)]
	public float Speed = 0.07f;
	private float AuxSpeed;
	public float Sprint = 0.12f;

    public float health = 100f;
    public float dmgCounter = 100f;
    private float initialDmgCounter = 0;

    public float Stamina = 200.0f;
    private float StaminaTimeRecover;
    private bool OffSetSprint = false;

    //  VARIABLES FOR GUNS
    private Rigidbody2D rb2dBullet;
	private GameObject bulletObject;
	private float initialBulletTime;
	private float Counter;


	[Header("Variables for guns:")]
	[Space(10)]
	public int weaponSelected = 0;
	public Transform firePoint;
	private static Weapon[] PlayerArrayWeapon;
	public Weapon weaponUsing;
	private int AuxRounds;
	public int TotalAmmo;
	public int Rounds;


    [Header("Variables for ArrayItems:")]
    [Space(10)]
    public int ItemSelected = 0;
    public static InventorySlot[] PlayerArrayItem;
    public InventorySlot itemUsing;

    //VARIABLES FOR OBJECTS
    [Header("Variables for objects & keys:")]
	[Space(10)]
    public int keys = 0;
	public int DroppedSouls = 0;
    private bool PickSoul = true;


    //VARIABLES FOR ANIMATIONS
    private Animator animator;
    private int moveParamID;


    void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;
		AuxSpeed = Speed;
        // GET ANIMATOR COMPONENTS

        animator = GetComponent<Animator>();
        moveParamID = Animator.StringToHash("Moving");

        //	SET WEAPON STATS TO AUXILIARS
        PlayerArrayWeapon = WeaponsArray.ArrayWeapon;
        //	SET ITEM STATS TO AUXILIARS
        PlayerArrayItem = ItemsArray.ArrayItemSlot;
		//	SET WEAPON USING VARIABLES
		Rounds = AuxRounds;
	}

    private void Update()
    {
        if (health <= 0) Destroy(gameObject);

        // UPDATE WEAPON USING & WEAPON VARIABLES
        GunsSwap();
        weaponUsing = PlayerArrayWeapon[weaponSelected];

        Rounds = weaponUsing.Rounds;
        TotalAmmo = weaponUsing.TotalAmmo;
        AuxRounds = weaponUsing.MaxRounds;

        //  RELOAD
        Reloading();

        //  UPDATE ITEM USING VARIABLES
        ItemsSwap();
        itemUsing = PlayerArrayItem[ItemSelected];

        if (Counter >= StaminaTimeRecover && Stamina < 200f && OffSetSprint)
        {
            Stamina += 0.2f;
            StaminaTimeRecover = Counter;
        }
        if (Counter >= StaminaTimeRecover + 80f && Stamina < 200f && !OffSetSprint)
        {
            OffSetSprint = true;
        }
    }

	private void FixedUpdate()
	{
		fixedDelta = Time.fixedDeltaTime * 1000.0f;
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

        if ((Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.Keypad0))) && Stamina > 0)
        {
            if ((Movement.x != 0 && Movement.y != 0) || Movement.y != 0)
            {
                Speed = Sprint;
                OffSetSprint = false;
                Stamina -= 1.5f;
            }
        }
        else Speed = AuxSpeed;

        if (rb2d.velocity.x > Speed || rb2d.velocity.x < Speed)
        {
            rb2d.velocity = Vector2.zero;
            animator.SetBool("Moving", false);
        }
		rb2d.AddForce(Movement * Speed * fixedDelta, ForceMode2D.Impulse);
        animator.SetBool("Moving", true);
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
		//bulletObject = Instantiate(weaponUsing.Bullet, firePoint.position, firePoint.rotation);
  //      bulletObject.GetComponent<Bullet>().PlayerShoot = true;
		//rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
		//rb2dBullet.AddForce(firePoint.up * weaponUsing.BulletSpeed, ForceMode2D.Impulse);
		//Destroy(bulletObject, weaponUsing.Range);
  //      weaponUsing.Rounds--;
	}
    void Reloading()
    {
		if (weaponUsing.Rounds <= 0 && weaponUsing.TotalAmmo <= 0) return;

		if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return)) && weaponUsing.TotalAmmo >= 0 && weaponUsing.Rounds < weaponUsing.MaxRounds)
		{
			Debug.Log("Reloading: Manual;");		
			weaponUsing.TotalAmmo += weaponUsing.Rounds;
			if (weaponUsing.TotalAmmo < weaponUsing.MaxRounds)
			{
				weaponUsing.Rounds = weaponUsing.TotalAmmo;
				weaponUsing.TotalAmmo -= weaponUsing.Rounds;
			}
			else
			{
				weaponUsing.Rounds = AuxRounds;
				weaponUsing.TotalAmmo -= weaponUsing.Rounds;
			}
		} 
		else if (weaponUsing.Rounds == 0 && weaponUsing.TotalAmmo > 0)
		{
			Debug.Log("Reloading: Automatic;");
			weaponUsing.Rounds = AuxRounds;
			weaponUsing.TotalAmmo -= weaponUsing.Rounds;
		}
	}
	void GunsSwap()
	{
        //float MouseInput = Input.GetAxis("Mouse ScrollWheel");
		string InputKey = Input.inputString;
        if (InputKey == "1" || InputKey == "2" || InputKey == "3" ||InputKey == "4")
        {
            if (PlayerArrayWeapon[int.Parse(InputKey) - 1] == null) return;
        
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
                case "4":
                    weaponSelected = 3;
                    break;
                default:
                    break;
            }
        }
		else return;
	}
    void ItemsSwap()
    {
        string InputKey = Input.inputString;
        if (InputKey == "5" || InputKey == "6" || InputKey == "7")
        {
            if (PlayerArrayWeapon[int.Parse(InputKey) - 1] == null) return;

            Debug.Log("Item Slot Swap to: " + InputKey);
            
            switch (InputKey)
            {
                case "5":
                    ItemSelected = 0;
                    break;
                case "6":
                    ItemSelected = 1;
                    break;
                case "7":
                    ItemSelected = 2;
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

        if (collision.gameObject.tag == "Bullet" && !collision.gameObject.GetComponent<Bullet>().PlayerShoot)
        {
            Destroy(collision.gameObject);
            health -= weaponUsing.Damage;
            Debug.Log("Player got shoot;");
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
            health += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Key_Object" && Input.GetKey(KeyCode.E))
        {
            keys++;
            Destroy(collision.gameObject);
        }
		if (collision.gameObject.tag == "Soul" && PickSoul == true)
		{
			Destroy(collision.gameObject);
			DroppedSouls++;
		}
	}
}
