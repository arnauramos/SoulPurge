using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlaceholder : MonoBehaviour
{
	public Weapon[] ArrWeapon;

	public static Weapon[] ArrayWeapon;

	public void Awake()
	{
		ArrayWeapon = ArrWeapon;
	}
}
