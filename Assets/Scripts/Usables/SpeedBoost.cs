using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.speedBoost += this.value;
        PlayerManager.Instance.ResetSpeedBoost();
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "SpeedBoost";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 0.25f;
        //this.doesExpire;
        //this.duration;
    }
}
