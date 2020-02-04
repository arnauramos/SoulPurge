using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapons/WeaponScriptableObject", order = 1)]
public class Weapon : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public GameObject Bullet;
    public Vector3 firePoint;
    public int Ammo;
    public float Range;
    public float Damage;
    public float FireRate;
}