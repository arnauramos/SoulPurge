using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoost : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.shootingBoost += this.value;
        PlayerManager.Instance.ResetShootingBoost();
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "ShootingBoost";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 0.25f;
        //this.doesExpire;
        //this.duration;
    }
}
