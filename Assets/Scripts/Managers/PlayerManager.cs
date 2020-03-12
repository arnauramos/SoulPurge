﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	// RESET TIMER


	// VARIABLES
	[Header("Variables for player:")]
	public float health;//
	public float maxHeath;
	public float resistance;
	public float stamina;//
	public float maxStamina;//
	public float staminaRegeneration;//
	public float speed;//
	public float maxSpeed;//
	public float sprint;//
	public float speedBoost;
	public float shootingBoost;
	public int souls;//
	public int maxSouls;
	public int money;//
	public int totalAmmo;//
	public int AuxGO;
	public bool usePriority;

	public List<Gun> PlayerGunList;
	public int weaponSelected = 0;

	public List<Usable> PlayerUsableList;
	public int usableSelected = 0;

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

	public void addGun(Gun _new)
	{
		int i = 0;
		foreach (Gun AuxGun in PlayerGunList)
		{
			if (AuxGun == null) PlayerGunList[i] = _new;
			i++;
		}
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
}
