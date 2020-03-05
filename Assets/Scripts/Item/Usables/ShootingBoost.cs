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
    protected override void CheckItem()
    {
        if (ammount <= 0) return;
        ammount--;
    }
    public override void Use()
    {
        this.itemName = "ShootingBoost";
        this.value = 0.25f;

        CheckItem();
        PlayerManager.Instance.shootingBoost += this.value;
        PlayerManager.Instance.ResetShootingBoost();
    }
}
