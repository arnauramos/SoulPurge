using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationBoost : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.staminaRegeneration += this.value;
        PlayerManager.Instance.ResetStaminaRegeneration();
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "RegenerationBoost";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 0.25f;
        //this.doesExpire;
        //this.duration;
    }
}
