using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceBoost : Usable
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
        this.itemName = "ResistanceBoost";
        this.value = 0.25f;

        if (!CheckItem()) return;

        PlayerManager.Instance.resistance += this.value;
        PlayerManager.Instance.ResetResistance();
    }
}
