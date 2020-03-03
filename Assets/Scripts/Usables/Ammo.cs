using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.addTotalAmmo((int)this.value);
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "Ammo";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 30;
        //this.doesExpire;
        //this.duration;
    }

}
