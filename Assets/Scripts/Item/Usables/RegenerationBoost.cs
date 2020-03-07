using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationBoost : Usable
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
        this.itemName = "RegenerationBoost";
        this.value = 0.25f;

        if (!CheckItem()) return;

        PlayerManager.Instance.staminaRegeneration += this.value;
        PlayerManager.Instance.ResetStaminaRegeneration();
    }
}
