using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : Usable
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
        this.itemName = "Bandage";
        this.value = 5;

        if (!CheckItem()) return;
        PlayerManager.Instance.addHealth(this.value);
    }
}
