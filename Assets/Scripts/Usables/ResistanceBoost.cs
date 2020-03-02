using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceBoost : Usable
{
    public override void Use()
    {
        PlayerManager.Instance.resistance += this.value;
        PlayerManager.Instance.ResetResistance();
    }
    void Start()
    {
        //this.sprite;
        this.itemName = "ResistanceBoost";
        //this.itemDescription;
        //this.price;
        //this.ammount;
        this.value = 0.25f;
        //this.doesExpire;
        //this.duration;
    }
}
