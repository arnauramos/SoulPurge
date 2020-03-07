using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Usable
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
        this.itemName = "Potion";
        this.value = 15;

        if (!CheckItem()) return;

        PlayerManager.Instance.addHealth(this.value);
    }
}
