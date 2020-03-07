using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : Usable
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
        this.itemName = "ExtraHealth";
        this.value = 25;

        if (!CheckItem()) return;

        PlayerManager.Instance.addExtraHealth(this.value);
    }
}
