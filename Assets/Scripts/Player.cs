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
    public int weaponUsingINT;
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
	private bool PickSoul = true;

	//VARIABLES FOR ANIMATIONS
	private Animator animator;
	private int moveParamID;

    // VARIABLES FOR DIALOGUE
    private DialogueScript dialoguescript;

    // VARIABLES FOR SOUNDS
    private float nextStep = 0f;
    private bool insideHouse = false;

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
        weaponUsingINT = PlayerManager.Instance.weaponSelected;

        PlayerSceneManager.Instance.isSceneHostile();
	}

	private void Update()
	{
		if (PlayerManager.Instance.health <= 0 && !PlayerManager.Instance.IMMORTAL) playerDie();

		// UPDATE WEAPON USING
		GunsSwap();
        if (PlayerManager.Instance.weaponSelected < 0)
        {
            weaponUsing = null;
            weaponGraphics.sprite = null;
        }
        else
        {
            weaponUsing = PlayerManager.Instance.PlayerGunList[PlayerManager.Instance.weaponSelected];
            weaponGraphics.sprite = weaponUsing.sprite;
        }

        //  UPDATE ITEM USING VARIABLES
        ItemsSwap();
        if (PlayerManager.Instance.usableSelected < 0)
        {
            itemUsing = null;
        }
        else
        {
            itemUsing = PlayerManager.Instance.PlayerUsableList[PlayerManager.Instance.usableSelected];
        }
        // WEAPON VARIABLES
        if (weaponUsing == null)
        {
            Rounds = 0;
            AuxRounds = 0;
        }
        else
        {
            Rounds = weaponUsing.Rounds;
            AuxRounds = weaponUsing.MaxRounds;
        }


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

		PlayerManager.Instance.reloading = reloading;
        PlayerManager.Instance.usePriority = false;
    }

    private void playerDie()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sounds.PlayerDie);
        Destroy(gameObject);
        MusicManager.Instance.PlaySong(MusicManager.Songs.GameOver);
        PlayerSceneManager.Instance.goLastScene();
    }

    private void FixedUpdate()
	{
		fixedDelta = Time.fixedDeltaTime * 1000.0f;
		Counter = Time.time * fixedDelta;
		PlayerMovement();
		PlayerManager.Instance.SetPlayerPosition(transform.position);
		PlayerAim();

		SpawnerManager.Instance.Spawner();
		SpawnerManager.Instance.EnemyChecker();

		if (weaponUsing == null) return;
		if (Counter >= ReloadingCounter && reloading == true) reloading = false;
		else
		{
			if (Counter >= initialBulletTime && Input.GetMouseButton(0))
			{
				if (weaponUsing.Rounds <= 0) return;

				if (reloading == true) return;

				Shooting();

				initialBulletTime = Counter + (weaponUsing.FireRate / PlayerManager.Instance.shootingBoost);
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
			if (Movement.y != 0 || Movement.x != 0)
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

        // STEP SOUNDS
        if (Movement.y >= 0.5f || Movement.x >= 0.5f || Movement.y <= -0.5f || Movement.x <= -0.5f)
        {  
            if (nextStep < 0.5f / PlayerManager.Instance.speed / PlayerManager.Instance.speedBoost)
            {
                nextStep += Time.fixedDeltaTime;
            }
            else
            {
                if (insideHouse)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.WoodenSteps);
                }
                else
                {
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.Steps);
                }
                nextStep = 0;
            }
        }
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
        SoundManager.Instance.PlaySound(SoundManager.Sounds.Shooting);
        Destroy(bulletObject, weaponUsing.Range);
		weaponUsing.Rounds--;
        DataManager.Instance.BulletsShot += 1;
	}
	void Reloading()
	{
        if (PlayerManager.Instance.playerDisabled || weaponUsing == null)
        {
            return;
        }
        if (weaponUsing.Rounds <= 0 && PlayerManager.Instance.totalAmmo <= 0) { PlayerManager.Instance.totalAmmo = 0; return; }

        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Return)) && PlayerManager.Instance.totalAmmo > 0 && weaponUsing.Rounds < weaponUsing.MaxRounds)
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
            if (weaponUsing.gameObject.GetComponent<Gun>().itemName != "Axe")
            {
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Reloading);
            }
            reloading = true;
        }
		else if (weaponUsing.Rounds == 0 && PlayerManager.Instance.totalAmmo > 0)
		{
			Debug.Log("Reloading: Automatic;");
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
            if (weaponUsing.gameObject.GetComponent<Gun>().itemName != "Axe")
            {
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Reloading);
            }
            reloading = true;
        }
    }
    void GunsSwap()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }

        // CHECK IF PLAYER HAS ANY WEAPON
        bool weaponFound = false;
        for (int i = 0; i < PlayerManager.Instance.PlayerGunList.Capacity; i++)
        {
            if (PlayerManager.Instance.PlayerGunList[i] != null)
            {
                weaponFound = true;
            }
        }
        if (!weaponFound)
        {
            return;
        }


        float MouseInput = Input.GetAxis("Mouse ScrollWheel");
        // SCROLLWHEEL UP
        if (MouseInput > 0f)
        {
            if (weaponUsingINT >= PlayerManager.Instance.PlayerGunList.Capacity - 1)
            {
                weaponUsingINT = 0;
            }
            else
            {
                weaponUsingINT++;
            }
            while (PlayerManager.Instance.PlayerGunList[weaponUsingINT] == null)
            {
                weaponUsingINT++;
                if (weaponUsingINT > PlayerManager.Instance.PlayerGunList.Capacity - 1)
                {
                    weaponUsingINT = 0;
                }
            }
            PlayerManager.Instance.weaponSelected = weaponUsingINT;
        }
        // SCROLLWHEEL DOWN
        if (MouseInput < 0f)
        {
            if (weaponUsingINT <= 0)
            {
                weaponUsingINT = PlayerManager.Instance.PlayerGunList.Capacity - 1;
            }
            else
            {
                weaponUsingINT--;
            }
            while (PlayerManager.Instance.PlayerGunList[weaponUsingINT] == null)
            {
                weaponUsingINT--;
                if (weaponUsingINT < 0)
                {
                    weaponUsingINT = PlayerManager.Instance.PlayerGunList.Capacity - 1;
                }
            }
            PlayerManager.Instance.weaponSelected = weaponUsingINT;
        }

        //string InputKey = Input.inputString;
        //if (InputKey == "1" || InputKey == "2" || InputKey == "3" ||InputKey == "4")
        //{
        //    if (PlayerManager.Instance.PlayerGunList[int.Parse(InputKey) - 1] == null) return;

        //	Debug.Log("Gun Swap to: " + InputKey);
        //	switch (InputKey)
        //	{
        //		case "1":
        //			PlayerManager.Instance.weaponSelected = 0;
        //			break;
        //		case "2":
        //			PlayerManager.Instance.weaponSelected = 1;
        //			break;
        //		case "3":
        //			PlayerManager.Instance.weaponSelected = 2;
        //			break;
        //		case "4":
        //			PlayerManager.Instance.weaponSelected = 3;
        //			break;
        //		default:
        //			break;
        //	}
        //}
        //else return;
    }
	void ItemsSwap()
	{
        if (PlayerManager.Instance.playerDisabled)
        {
            return;
        }
        string InputKey = Input.inputString;
		if (InputKey == "1" || InputKey == "2" || InputKey == "3" || InputKey == "4" || InputKey == "5")
		{
			if (PlayerManager.Instance.PlayerUsableList[int.Parse(InputKey) - 1] == null) return;

			Debug.Log("Item Slot Swap to: " + InputKey);
			switch (InputKey)
			{
				case "1":
					PlayerManager.Instance.usableSelected = 0;
                    UseItem();
					break;
				case "2":
					PlayerManager.Instance.usableSelected = 1;
                    UseItem();
                    break;
				case "3":
					PlayerManager.Instance.usableSelected = 2;
                    UseItem();
					break;
				case "4":
					PlayerManager.Instance.usableSelected = 3;
                    UseItem();
					break;
				case "5":
					PlayerManager.Instance.usableSelected = 4;
                    UseItem();
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
        if (PlayerManager.Instance.usePriority == false)
		{
            itemUsing = PlayerManager.Instance.PlayerUsableList[PlayerManager.Instance.usableSelected];
            if (itemUsing == null)
            {
                return;
            }
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
                float Dmg = collision.gameObject.GetComponent<Enemy>().dmg;
                PlayerManager.Instance.substrHealth(Dmg - (Dmg * PlayerManager.Instance.resistance));
				initialDmgCounter = dmgCounter + collision.gameObject.GetComponent<Enemy>().AttackRate;
                if (PlayerManager.Instance.health <= 0) SoundManager.Instance.PlaySound(SoundManager.Sounds.PlayerDie);
                SoundManager.Instance.PlaySound(SoundManager.Sounds.PlayerDamage);

            }
		}

		if (collision.gameObject.tag == "Bullet" && !collision.gameObject.GetComponent<Bullet>().PlayerShoot)
		{
			Destroy(collision.gameObject);
            float gunDmg = ItemsManager.Instance.GunsList[0].Damage;
            PlayerManager.Instance.substrHealth(gunDmg - (gunDmg * PlayerManager.Instance.resistance));
            if (PlayerManager.Instance.health <= 0) SoundManager.Instance.PlaySound(SoundManager.Sounds.PlayerDie);
            SoundManager.Instance.PlaySound(SoundManager.Sounds.PlayerDamage);
            Debug.Log("Player got shoot;");
		}

		if (collision.gameObject.tag == "Safe_Door" && ((PlayerManager.Instance.keys >= 3 && PlayerSceneManager.Instance.ZoneIsHostile) || !PlayerSceneManager.Instance.ZoneIsHostile))
		{
            if (PlayerSceneManager.Instance.ZoneIsHostile)
            {
                PlayerManager.Instance.keys -= 3;
                MusicManager.Instance.PlaySong(MusicManager.Songs.SafeZone);

            }
            else
            {
                MusicManager.Instance.PlaySong(MusicManager.Songs.HostileZone);
            }
            if (collision.gameObject.name == "SafeDoor_Left")
            {
                PlayerSceneManager.Instance.goBackScene(PlayerSceneManager.Instance.getActualScene());
            }
            else if (collision.gameObject.name == "SafeDoor_Right")
            {
                PlayerSceneManager.Instance.goFrontScene(PlayerSceneManager.Instance.getActualScene());
            }
        }
		if (collision.gameObject.tag == "Door")
		{
			EnviromentManager.Instance.manageDoor(collision.gameObject);
		}
	}

    // RANDOM SOUL PICKUP SOUND
    private void PlaySoulPickup()
    {
        int soulPickupSound = UnityEngine.Random.Range(0, 3);
        switch (soulPickupSound)
        {
            case 1:
                SoundManager.Instance.PlaySound(SoundManager.Sounds.SoulPickup1);
                break;
            case 2:
                SoundManager.Instance.PlaySound(SoundManager.Sounds.SoulPickup2);
                break;
            case 3:
                SoundManager.Instance.PlaySound(SoundManager.Sounds.SoulPickup3);
                break;
            default:
                break;
        }
    }

    // PICK UP OBJECTS 
    private void OnTriggerStay2D(Collider2D collision)
	{
        // PICK UP USABLE
		if (collision.gameObject.tag == "Usable") 
		{
            int id = -1;
            for (int i = 0; i < ItemsManager.Instance.UsablesList.Capacity; i++)
            {
                if (collision.gameObject.GetComponent<Usable>().itemName == ItemsManager.Instance.UsablesList[i].itemName)
                {
                    id = i;
                    break;
                } 
            }
            PlayerManager.Instance.addUsableById(id, 1);
            Destroy(collision.gameObject);
		}
        // PICK UP KEY
		if (collision.gameObject.tag == "Key_Object")
		{
            PlayerManager.Instance.keys++;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.KeyPickup);
            Destroy(collision.gameObject);
		}
        // PICK UP SOUL
		if (collision.gameObject.tag == "Soul" && PickSoul == true)
		{
			Destroy(collision.gameObject);
            PlaySoulPickup();
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

		if (collision.gameObject.tag == "Inside")
		{
			EnviromentManager.Instance.openRoof();
            insideHouse = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Inside")
		{
			EnviromentManager.Instance.closeRoof();
            insideHouse = false;
        }
    }
}
