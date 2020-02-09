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

	//public WeaponArray_Test[] ArrWeaponTest;


	//void Awake()
	//{
	//    for (int i = 0; i < ArrWeaponTest.Length; i++)
	//    {
	//        ArrWeapon[i].Name = ArrWeaponTest[i].Name;
	//        ArrWeapon[i].Sprite = ArrWeaponTest[i].Sprite;
	//        ArrWeapon[i].Bullet = ArrWeaponTest[i].Bullet;
	//        ArrWeapon[i].firePoint = ArrWeaponTest[i].firePoint;
	//        ArrWeapon[i].Ammo = ArrWeaponTest[i].Ammo;
	//        ArrWeapon[i].Range = ArrWeaponTest[i].Range;
	//        ArrWeapon[i].Damage = ArrWeaponTest[i].Damage;
	//        ArrWeapon[i].FireRate = ArrWeaponTest[i].FireRate;
	//    }
	//}
}
