using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.addHealth(this.value);
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "Bandage";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 5;
        //this.doesExpire;
        //this.duration;
    }

}
