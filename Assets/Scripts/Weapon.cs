using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapons/WeaponScriptableObject", order = 1)]
public class Weapon : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public GameObject Bullet;
	public Vector2 BulletSpeed;
	//public Transform FirePoint;
	public Vector3 FirePoint;
	public int Rounds;
	public int MaxRounds;
	public int TotalAmmo;
	public float Range;
	public float Damage;
	public float FireRate;

}