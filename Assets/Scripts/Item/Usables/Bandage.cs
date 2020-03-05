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
    protected override void CheckItem()
    {
        if (ammount <= 0) return;
        ammount--;
    }
    public override void Use()
    {
        this.itemName = "Bandage";
        this.value = 5;

        CheckItem();
        PlayerManager.Instance.addHealth(this.value);
    }
}
