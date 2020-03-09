using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public GameObject Bullet;
    public Vector2 BulletSpeed;
    //public Transform FirePoint;
    public Vector3 FirePoint;
    public int Rounds;
    public int MaxRounds;
    public float Range;
    public float Damage;
    public float FireRate;
    public float reloadTime;
}
