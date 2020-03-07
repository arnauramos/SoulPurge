using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Usable
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
        this.itemName = "SpeedBoost";
        this.value = 0.25f;

        if (!CheckItem()) return;
        PlayerManager.Instance.speedBoost += this.value;
        PlayerManager.Instance.ResetSpeedBoost();
    }
}
