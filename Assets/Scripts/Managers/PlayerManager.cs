using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	// RESET TIMER

	public bool IMMORTAL;

	// VARIABLES
	[Header("Variables for player:")]
	public float health;
	public float maxHeath;
	public float resistance;//
	public float stamina;
	public float maxStamina;
	public float staminaRegeneration;
	public float speed;
	public float maxSpeed;
	public float sprint;
	public float speedBoost;//
	public float shootingBoost;//
	public int souls;
	public int maxSouls;
	public int money;
	public int totalAmmo;
	public int AuxGO;
	public bool usePriority;
	public bool reloading;
	public bool playerDisabled;

	public Vector2 PlayerPosition;

	public List<Gun> PlayerGunList;
	public int weaponSelected = 0;

	public List<Usable> PlayerUsableList;
	public int usableSelected = 0;

    //public bool tutorialDone = false; 

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
	}

	public void substrStamina(float value)
	{
		stamina -= value;
	}

	public void substrTotalAmmo(int value)
	{
		totalAmmo -= value;
	}

	//RESET (work in progress)
	public void ResetResistance()
	{
		//resistance = 1;
	}
	public void ResetStaminaRegeneration()
	{
		//staminaRegeneration = 1;
	}
	public void ResetSpeedBoost()
	{
		//speedBoost = 1;
	}
	public void ResetShootingBoost()
	{
		//shootingBoost = 1;
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
        resistance = 0;
        stamina = 400;
        maxStamina = 400;
        staminaRegeneration = 0.2f;
        speed = 1.25f;
        maxSpeed = 1.25f;
        sprint = 2;
        speedBoost = 1;
        shootingBoost = 1;
        souls = 0;
        maxSouls = 100;
        money = 0;
        totalAmmo = 600;
        AuxGO = 0;
        usePriority = false;
        reloading = false;
        playerDisabled = false;
    }
}
