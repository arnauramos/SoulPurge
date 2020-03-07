using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoost : Usable
{
    //void Start()
    //{
    //    //this.sprite;
    //    //this.itemDescription;
    //    //this.price;
    //    //this.ammount;
    //    //this.doesExpire;
    //    //this.duration;
    //}
    public override void Use()
    {
        this.itemName = "ShootingBoost";
        this.value = 0.25f;

        if (!CheckItem()) return;

        PlayerManager.Instance.shootingBoost += this.value;
        PlayerManager.Instance.ResetShootingBoost();
    }
}
