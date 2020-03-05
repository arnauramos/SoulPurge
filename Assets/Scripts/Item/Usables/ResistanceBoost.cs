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
    protected override void CheckItem()
    {
        if (ammount <= 0) Destroy(gameObject);
        ammount--;
    }
    public override void Use()
    {
        this.itemName = "ResistanceBoost";
        this.value = 0.25f;

        CheckItem();
        PlayerManager.Instance.resistance += this.value;
        PlayerManager.Instance.ResetResistance();
    }
}
