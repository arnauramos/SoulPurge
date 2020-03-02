using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.addHealth(this.value);
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "Potion";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 15;
        //this.doesExpire;
        //this.duration;
    }

}
