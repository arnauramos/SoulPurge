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

	private float fixedDelta;
	
	[Header("Counters for player:")]
	[Space(5)]
	public float dmgCounter = 100f;
	private float initialDmgCounter = 0;
	private float StaminaTimeRecover;
	private bool OffSetSprint = false;

	//  VARIABLES FOR GUNS
	private Rigidbody2D rb2dBullet;
	private GameObject bulletObject;
	private float initialBulletTime;
	private float Counter;

	[Header("Variables for guns:")]
	[Space(10)]
	public Transform firePoint;
    public GameObject weaponGraphicsObject;
    private SpriteRenderer weaponGraphics;
	public Gun weaponUsing;
	private int AuxRounds;
	public int Rounds;

    public bool reloading;
    private float ReloadingCounter;


    [Header("Variables for ArrayItems:")]
	[Space(10)]
	public Usable itemUsing;

	[Header("Variables for objects & keys:")]
	[Space(10)]
	public int keys = 0;
	private bool PickSoul = true;

	//VARIABLES FOR ANIMATIONS
	private Animator animator;
	private int moveParamID;

    // VARIABLES FOR DIALOGUE
    private DialogueScript dialoguescript;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		Movement = Vector2.zero;

		// GET ANIMATOR COMPONENTS
		animator = GetComponent<Animator>();
		moveParamID = Animator.StringToHash("Moving");

        weaponGraphics = weaponGraphicsObject.GetComponent<SpriteRenderer>();
		//	SET WEAPON USING VARIABLES
		Rounds = AuxRounds;
        reloading = false;
	}

	private void Update()
	{
		if (PlayerManager.Instance.health <= 0 && !PlayerManager.Instance.IMMORTAL) playerDie();

		// UPDATE WEAPON USING
		GunsSwap();
		weaponUsing = PlayerManager.Instance.PlayerGunList[PlayerManager.Instance.weaponSelected];
        weaponGraphics.sprite = weaponUsing.sprite;

		//  UPDATE ITEM USING VARIABLES
		ItemsSwap();
		itemUsing = PlayerManager.Instance.PlayerUsableList[PlayerManager.Instance.usableSelected];

		// WEAPON VARIABLES
		Rounds = weaponUsing.Rounds;
		AuxRounds = weaponUsing.MaxRounds;

        //  RELOAD
        Reloading();

		if (Counter >= StaminaTimeRecover && PlayerManager.Instance.stamina < PlayerManager.Instance.maxStamina && OffSetSprint)
		{
			PlayerManager.Instance.addStamina(PlayerManager.Instance.staminaRegeneration);
			StaminaTimeRecover = Counter;
		}
		if (Counter >= StaminaTimeRecover + 80f && PlayerManager.Instance.stamina < PlayerManager.Instance.maxStamina && !OffSetSprint)
		{
			OffSetSprint = true;
		}

		UseItem();

		PlayerManager.Instance.reloading = reloading;
        PlayerManager.Instance.usePriority = false;
    }

    private void playerDie()
    {
        Destroy(gameObject);
        PlayerSceneManager.Instance.goLastScene();
    }

    private void FixedUpdate()
	{
		fixedDelta = Time.fixedDeltaTime * 1000.0f;
		Counter = Time.time * fixedDelta;
		PlayerMovement();
		PlayerManager.Instance.SetPlayerPosition(transform.position);
		PlayerAim();
		SpawnerManager.Instance.CheckRawPosition();

		if (Counter >= ReloadingCounter && reloading == true) reloading = false;
		else
		{
			if (Counter >= initialBulletTime / PlayerManager.Instance.shootingBoost && Input.GetMouseButton(0))
			{
				if (weaponUsing.Rounds <= 0) return;

				if (reloading == true) return;

				Shooting();

				initialBulletTime = Counter + weaponUsing.FireRate;
				ReloadingCounter = Counter + weaponUsing.reloadTime;
			}
		}
	}

	void PlayerMovement()
	{
        if (PlayerManager.Instance.playerDisabled) {
            rb2d.velocity = Vector2.zero;
            return;
        }
		Movement.x = Input.GetAxis("Horizontal");
		Movement.y = Input.GetAxis("Vertical");

		if ((Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.Keypad0))) && PlayerManager.Instance.stamina > 0)
		{
			if (Movement.y != 0)
			{
				PlayerManager.Instance.changeSpeed(PlayerManager.Instance.sprint);
				OffSetSprint = false;
				PlayerManager.Instance.substrStamina(1.5f);
			}
			else PlayerManager.Instance.changeSpeed(PlayerManager.Instance.maxSpeed);
		}
		else PlayerManager.Instance.changeSpeed(PlayerManager.Instance.maxSpeed);

		if (rb2d.velocity.x > PlayerManager.Instance.speed || rb2d.velocity.x < PlayerManager.Instance.speed)
		{
			rb2d.velocity = Vector2.zero;
			animator.SetBool("Moving", false);
		}
		rb2d.AddForce(Movement * PlayerManager.Instance.speed * PlayerManager.Instance.speedBoost * fixedDelta, ForceMode2D.Impulse);
		animator.SetBool("Moving", true);
        DataManager.Instance.TrackDistance(transform.position);
	}
	void PlayerAim()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

		Vector2 lookDirection = mousePosition - rb2d.position;
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
		rb2d.rotation = angle;
	}
	void Shooting()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        bulletObject = Instantiate(weaponUsing.Bullet, firePoint.position, firePoint.rotation);
		bulletObject.GetComponent<Bullet>().PlayerShoot = true;
		rb2dBullet = bulletObject.GetComponent<Rigidbody2D>();
		rb2dBullet.AddForce(firePoint.up * weaponUsing.BulletSpeed, ForceMode2D.Impulse);
		Destroy(bulletObject, weaponUsing.Range);
		weaponUsing.Rounds--;
        DataManager.Instance.BulletsShot += 1;
	}
	void Reloading()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        if (weaponUsing.Rounds <= 0 && PlayerManager.Instance.totalAmmo <= 0) { PlayerManager.Instance.totalAmmo = 0; return; }

        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return)) && PlayerManager.Instance.totalAmmo >= 0 && weaponUsing.Rounds < weaponUsing.MaxRounds)
		{
			Debug.Log("Reloading: Manual;");
			PlayerManager.Instance.addTotalAmmo(weaponUsing.Rounds);
			if (PlayerManager.Instance.totalAmmo < weaponUsing.MaxRounds)
			{
				weaponUsing.Rounds = PlayerManager.Instance.totalAmmo;
				PlayerManager.Instance.substrTotalAmmo(weaponUsing.Rounds);
			}
			else
			{
				weaponUsing.Rounds = AuxRounds;
				PlayerManager.Instance.substrTotalAmmo(weaponUsing.Rounds);
			}
            reloading = true;
        }
		else if (weaponUsing.Rounds == 0 && PlayerManager.Instance.totalAmmo > 0)
		{
			Debug.Log("Reloading: Automatic;");
			weaponUsing.Rounds = AuxRounds;
			PlayerManager.Instance.substrTotalAmmo(weaponUsing.Rounds);
            reloading = true;
        }
    }
	void GunsSwap()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        //float MouseInput = Input.GetAxis("Mouse ScrollWheel");
        string InputKey = Input.inputString;
		if (InputKey == "1" || InputKey == "2" || InputKey == "3" ||InputKey == "4")
		{
		    if (PlayerManager.Instance.PlayerGunList[int.Parse(InputKey) - 1] == null) return;
		
			Debug.Log("Gun Swap to: " + InputKey);
			switch (InputKey)
			{
				case "1":
					PlayerManager.Instance.weaponSelected = 0;
					break;
				case "2":
					PlayerManager.Instance.weaponSelected = 1;
					break;
				case "3":
					PlayerManager.Instance.weaponSelected = 2;
					break;
				case "4":
					PlayerManager.Instance.weaponSelected = 3;
					break;
				default:
					break;
			}
		}
		else return;
	}
	void ItemsSwap()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        string InputKey = Input.inputString;
		if (InputKey == "5" || InputKey == "6" || InputKey == "7" || InputKey == "8" || InputKey == "9")
		{
			if (PlayerManager.Instance.PlayerUsableList[int.Parse(InputKey) - 5] == null) return;

			Debug.Log("Item Slot Swap to: " + InputKey);
			switch (InputKey)
			{
				case "5":
					PlayerManager.Instance.usableSelected = 0;
					break;
				case "6":
					PlayerManager.Instance.usableSelected = 1;
					break;
				case "7":
					PlayerManager.Instance.usableSelected = 2;
					break;
				case "8":
					PlayerManager.Instance.usableSelected = 3;
					break;
				case "9":
					PlayerManager.Instance.usableSelected = 4;
					break;
				default:
					break;
			}
		}
		else return;
	}
	void UseItem()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerManager.Instance.usePriority == false)
		{
			if (itemUsing.ammount <= 0) { Debug.Log("Cantidad: " + itemUsing.ammount); return; }
			itemUsing.Use();
			Debug.Log("Ha sido usado el item: " + itemUsing.itemName + ". En la posición: " + PlayerManager.Instance.usableSelected + " del array");
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			dmgCounter = Time.time * fixedDelta;
			if (dmgCounter >= initialDmgCounter)
			{
				PlayerManager.Instance.substrHealth(collision.gameObject.GetComponent<Enemy>().dmg);
				initialDmgCounter = dmgCounter + collision.gameObject.GetComponent<Enemy>().AttackRate;
			}
		}

		if (collision.gameObject.tag == "Bullet" && !collision.gameObject.GetComponent<Bullet>().PlayerShoot)
		{
			Destroy(collision.gameObject);
			PlayerManager.Instance.substrHealth(weaponUsing.Damage);

			Debug.Log("Player got shoot;");
		}

		if (collision.gameObject.tag == "Safe_Door" /*&& keys == 3*/)
		{
            if (collision.gameObject.name == "SafeDoor_Left")
            {
                PlayerSceneManager.Instance.goBackScene(PlayerSceneManager.Instance.getActualScene());
            }
            else if (collision.gameObject.name == "SafeDoor_Right")
            {
                PlayerSceneManager.Instance.goFrontScene(PlayerSceneManager.Instance.getActualScene());
            }
        }
	}
	private void OnTriggerStay2D(Collider2D collision) //pillar objetos (Albert)
	{
		if (collision.gameObject.tag == "Object" && Input.GetKey(KeyCode.E)) //de momento object solo sera vendas, asi que sumara vida cuando se pille
		{
			PlayerManager.Instance.addHealth(10);
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
			PlayerManager.Instance.addSouls(1);
		}

		//USE PRIORITY SETTING
		if (collision.gameObject.tag == "SoulsExchange" || collision.gameObject.tag == "UsablesShop" || collision.gameObject.tag == "WeaponsShop" || collision.gameObject.tag == "Person") PlayerManager.Instance.usePriority = true;

        // SOULS EXCHANGE
		if (collision.gameObject.tag == "SoulsExchange" && Input.GetKey(KeyCode.E) && PlayerManager.Instance.usePriority == true)
		{
            InteractionManager.Instance.SoulsShop(collision.gameObject);
            //InteractionManager.Instance.SoulsExchange(PlayerManager.Instance.souls);
        }

        // USABLES SHOP
        if (collision.gameObject.tag == "UsablesShop" && Input.GetKey(KeyCode.E) && PlayerManager.Instance.usePriority == true)
        {
            InteractionManager.Instance.UsablesShop(collision.gameObject);
        }

        // WEAPONS SHOP
        if (collision.gameObject.tag == "WeaponsShop" && Input.GetKey(KeyCode.E) && PlayerManager.Instance.usePriority == true)
        {
            InteractionManager.Instance.WeaponsShop(collision.gameObject);
        }

        // PERSON
        if (collision.gameObject.tag == "Person" && Input.GetKey(KeyCode.E) && PlayerManager.Instance.usePriority == true)
        {
            dialoguescript = collision.gameObject.GetComponent<DialogueScript>();
            dialoguescript.playDialogue();
        }
    }
}
