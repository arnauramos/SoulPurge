using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.addExtraHealth(this.value);
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "ExtraHealth";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 25;
        //this.doesExpire;
        //this.duration;
    }

}
