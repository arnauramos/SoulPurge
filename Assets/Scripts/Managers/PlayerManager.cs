using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	// RESET TIMER

	public bool IMMORTAL;

	// VARIABLES
	[Header("Variables for player:")]
	public float health;
	public float maxHeath;
	public float stamina;
	public float maxStamina;
    public float speed;
	public float maxSpeed;
	public float sprint;
	public int souls;
	public int maxSouls;
	public int money;
	public int totalAmmo;
	public int AuxGO;
	public bool usePriority;
	public bool reloading;
	public bool playerDisabled;
    public int keys = 0;

    public Vector2 PlayerPosition;

	public List<Gun> PlayerGunList;
	public int weaponSelected = 0;

	public List<Usable> PlayerUsableList;
	public int usableSelected = 0;

    public bool tutorialDone = false;

    // BOOSTS
    public float resistance;
    public float staminaRegeneration;
    public float speedBoost;
    public float shootingBoost;

    public bool resistanceActive = false;
    public bool staminaRegenerationActive = false;
    public bool speedBoostActive = false;
    public bool shootingBoostActive = false;

    private float resistanceSeconds = 0;
    private float staminaRegenerationSeconds = 0;
    private float speedBoostSeconds = 0;
    private float shootingBoostSeconds = 0;

    private float resistanceActualSeconds = 0;
    private float staminaRegenerationActualSeconds = 0;
    private float speedBoostActualSeconds = 0;
    private float shootingBoostActualSeconds = 0;

    private float resistanceValue = 0;
    private float staminaRegenerationValue = 0;
    private float speedBoostValue = 0;
    private float shootingBoostValue = 0;




    void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Debug.Log("Error: Duplicated " + this + "in the scene");
		}
	}

    private void Update()
    {
        if (resistanceActive)
        {
            if (resistanceActualSeconds < resistanceSeconds)
            {
                resistanceActualSeconds += Time.deltaTime;
            }
            else
            {
                resistanceActive = false;
                resistance -= resistanceValue;
                resistanceActualSeconds = 0;
                resistanceSeconds = 0;
                resistanceValue = 0;
            }
        }
        if (staminaRegenerationActive)
        {
            if (staminaRegenerationActualSeconds < staminaRegenerationSeconds)
            {
                staminaRegenerationActualSeconds += Time.deltaTime;
            }
            else
            {
                staminaRegenerationActive = false;
                staminaRegeneration -= staminaRegenerationValue;
                staminaRegenerationActualSeconds = 0;
                staminaRegenerationSeconds = 0;
                staminaRegenerationValue = 0;
            }
        }
        if (speedBoostActive)
        {
            if (speedBoostActualSeconds < speedBoostSeconds)
            {
                speedBoostActualSeconds += Time.deltaTime;
            }
            else
            {
                speedBoostActive = false;
                speedBoost -= speedBoostValue;
                speedBoostActualSeconds = 0;
                speedBoostSeconds = 0;
                speedBoostValue = 0;
            }
        }
        if (shootingBoostActive)
        {
            if (shootingBoostActualSeconds < shootingBoostSeconds)
            {
                shootingBoostActualSeconds += Time.deltaTime;
            }
            else
            {
                shootingBoostActive = false;
                shootingBoost -= shootingBoostValue;
                shootingBoostActualSeconds = 0;
                shootingBoostSeconds = 0;
                shootingBoostValue = 0;
            }
        }
        if (SceneManager.GetActiveScene().name == "DEV_TileMap-ZonaHostil-3" && SpawnerManager.Instance.ActualRound >= 5)
        {
            Victory();
        }
    }

    private void Victory()
    {
        PlayerSceneManager.Instance.goFrontScene(SceneManager.GetActiveScene().buildIndex);
    }
    // ADD
    public void addHealth(float value)
	{
		if (health + value > maxHeath)
		{
			health = maxHeath;
		}
		else
		{
			health += value;
		}
	}
	public void addStamina(float value)
	{
		if (stamina + value > maxStamina)
		{
			stamina = maxStamina;
		}
		else
		{
			stamina += value;
		}
	}
	public void changeSpeed(float value)
	{
		speed = value;
	}
	public void addSouls(int value)
	{
		if (souls + value > maxSouls)
		{
			souls = maxSouls;
		}
		else
		{
			souls += value;
            DataManager.Instance.ObtainedSouls += value;
		}
	}
	public void addMoney(int value)
	{
		money += value;
	}
	public void addTotalAmmo(int value)
	{
		totalAmmo += value;
	}

	//  MANAGE ITEMS

	public int addGun(Gun _new)
	{
        for (int i = 0; i < PlayerGunList.Count; i++)
        {
            if (PlayerGunList[i] == _new)
            {
                return -1;
            }
        }
        for (int i = 0; i < PlayerGunList.Count; i++)
        {
            if (PlayerGunList[i] == null)
            {
                PlayerGunList[i] = _new;
                return 0;
            }
        }
        return -2;
    }

	public void addGun(Gun _original, Gun _new)
	{
		int i = 0;
		foreach (Gun AuxGun in PlayerGunList)
		{
			if (AuxGun == _original) PlayerGunList[i] = _new;
			i++;
		}
    }
    public int removeGun(int i)
    {
        PlayerGunList[i] = null;
        // update inventory
        for (int x = 0; x < PlayerGunList.Capacity; x++)
        {
            if (PlayerGunList[x] != null)
            {
                weaponSelected = x;
            }
        }
        if (weaponSelected == i)
        {
            weaponSelected = -1;
        }
        return 0;
    }

    // ADD USABLE
    public bool addUsable(Usable _new, int ammount)
    {
        bool Bought = false;
        bool Found = false;
        // CHECK INVENTORY
        for (int i = 0; i < PlayerUsableList.Count; i++)
        {
            if (PlayerUsableList[i] == _new)
            {
                Found = true;
                if (PlayerUsableList[i].ammount + ammount <= 99)
                {
                    // ADD ITEMS
                    PlayerUsableList[i].ammount += ammount;
                    Bought = true;
                    break;
                }
            }       
        }
        if (!Found)
        {
            for (int x = 0; x < PlayerUsableList.Count; x++)
            {
                if (PlayerUsableList[x] == null)
                {
                    // ADD ITEMS
                    PlayerUsableList[x] = _new;
                    PlayerUsableList[x].ammount = ammount;
                    Bought = true;
                    break;
                }
            }
        }
        if (!Bought)
        {
            // NO SPACE
        }
        return Bought;
    }
    public bool addUsableById(int _id, int ammount)
    {
        bool Bought = false;
        bool Found = false;
        if (_id < 0)
        {
            // NULL ID
            return false;
        }
        // CHECK INVENTORY
        for (int i = 0; i < PlayerUsableList.Count; i++)
        {
            if (PlayerUsableList[i] == ItemsManager.Instance.UsablesList[_id])
            {
                Found = true;
                if (PlayerUsableList[i].ammount + ammount <= 99)
                {
                    // ADD ITEMS
                    PlayerUsableList[i].ammount += ammount;
                    Bought = true;
                    break;
                }
            }
        }
        if (!Found)
        {
            for (int x = 0; x < PlayerUsableList.Count; x++)
            {
                if (PlayerUsableList[x] == null)
                {
                    // ADD ITEMS
                    PlayerUsableList[x] = ItemsManager.Instance.UsablesList[_id];
                    PlayerUsableList[x].ammount = ammount;
                    Bought = true;
                    break;
                }
            }
        }
        if (!Bought)
        {
            // NO SPACE
        }
        return Found;
    }
    public int removeUsable(int i, int ammount)
    {
        PlayerUsableList[i].ammount -= ammount;
        if (PlayerUsableList[i].ammount <= 0)
        {
            PlayerUsableList[i] = null;
        }
        return 0;
    }


    //EXTRA MAX
    public void addExtraHealth(float value)
	{
		maxHeath += value;
	}
	public void addExtraStamina(float value)
	{
		maxStamina += value;
	}
	public void addExtraSouls(int value)
	{
		maxSouls += value;
	}

	// SUBSTRACTS
	public void substrHealth(float value)
	{
		health -= value;
        if (health < 0)
        {
            health = 0;
        }
	}

	public void substrStamina(float value)
	{
		stamina -= value;
        if (stamina < 0)
        {
            stamina = 0;
        }
	}

	public void substrTotalAmmo(int value)
	{
		totalAmmo -= value;
	}

	//RESET
	public void UseResistance(float seconds, float value)
	{
        if (!resistanceActive)
        {
            resistance += value;
            resistanceActive = true;
            resistanceSeconds = seconds;
            resistanceValue = value;
        }
        else
        {
            resistanceActualSeconds = 0;
        }
    }
    public void UseStaminaRegeneration(float seconds, float value)
	{
        if (!staminaRegenerationActive)
        {
            staminaRegeneration += value;
            staminaRegenerationActive = true;
            staminaRegenerationSeconds = seconds;
            staminaRegenerationValue = value;
        }
        else
        {
            staminaRegenerationActualSeconds = 0;
        }

    }
	public void UseSpeedBoost(float seconds, float value)
	{
        if (!speedBoostActive)
        {
            speedBoost += value;
            speedBoostActive = true;
            speedBoostSeconds = seconds;
            speedBoostValue = value;
        }
        else
        {
            speedBoostActualSeconds = 0;
        }
    }
    public void UseShootingBoost(float seconds, float value)
	{
        if (!shootingBoostActive)
        {
            shootingBoost += value;
            shootingBoostActive = true;
            shootingBoostSeconds = seconds;
            shootingBoostValue = value;
        }
        else
        {
            shootingBoostActualSeconds = 0;
        }
    }

	public void SetPlayerPosition(Vector2 position)
	{
		PlayerPosition = position;
	}

	public Vector2 GetPlayerPosition()
	{
		return PlayerPosition;
	}

    // RESET VARIABLES
    public void reset()
    {
        health = 100;
        maxHeath = 100;
        stamina = 400;
        maxStamina = 400;
        speed = 1.25f;
        maxSpeed = 1.25f;
        sprint = 2;
        souls = 0;
        maxSouls = 100;
        money = 0;
        AuxGO = 0;
        usePriority = false;
        reloading = false;
        playerDisabled = false;
        keys = 0;

        // BOOSTS
        resetBoosts();
        // WEAPON INVENTORY
        resetWeaponsInventory();
        // USABLES INVENTORY
        resetUsablesInventory();
    }

    private void resetBoosts()
    {
        resistance = 0;
        staminaRegeneration = 0.6f;
        speedBoost = 1f;
        shootingBoost = 1f;

        resistanceActive = false;
        staminaRegenerationActive = false;
        speedBoostActive = false;
        shootingBoostActive = false;

        resistanceSeconds = 0;
        staminaRegenerationSeconds = 0;
        speedBoostSeconds = 0;
        shootingBoostSeconds = 0;

        resistanceActualSeconds = 0;
        staminaRegenerationActualSeconds = 0;
        speedBoostActualSeconds = 0;
        shootingBoostActualSeconds = 0;

        resistanceValue = 0;
        staminaRegenerationValue = 0;
        speedBoostValue = 0;
        shootingBoostValue = 0;
    }
    private void resetWeaponsInventory()
    {
        // START WITH A GUN
        totalAmmo = 260;
        for (int i = 0; i < ItemsManager.Instance.GunsList.Capacity; i++)
        {
            ItemsManager.Instance.GunsList[i].Rounds = ItemsManager.Instance.GunsList[i].MaxRounds;
        }
        for (int i = 0; i < PlayerGunList.Capacity; i++)
        {
            if (i == 0)
            {
                PlayerGunList[i] = ItemsManager.Instance.GunsList[i];
                PlayerGunList[i].Rounds = PlayerGunList[0].MaxRounds;
            }
            else
            {
                PlayerGunList[i] = null;
            }
        }
        weaponSelected = 0;
    }
    private void resetUsablesInventory()
    {
        for (int i = 0; i < ItemsManager.Instance.UsablesList.Capacity; i++)
        {
            ItemsManager.Instance.UsablesList[i].ammount = 0;
        }
        for (int i = 0; i < PlayerUsableList.Capacity; i++)
        {
            PlayerUsableList[i] = null;
        }
        usableSelected = -1;
    }

}
