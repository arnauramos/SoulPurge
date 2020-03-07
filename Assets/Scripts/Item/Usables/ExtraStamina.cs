﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraStamina : Usable
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
        this.itemName = "ExtraStamina";
        this.value = 50;

        if (!CheckItem()) return;

        PlayerManager.Instance.addExtraStamina(this.value);
    }
}