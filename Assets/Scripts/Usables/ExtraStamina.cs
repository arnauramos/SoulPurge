using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraStamina : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.addExtraStamina(this.value);
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "ExtraStamina";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 50;
        //this.doesExpire;
        //this.duration;
    }

}
