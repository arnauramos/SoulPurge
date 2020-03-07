using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Item
{
    public abstract void Use();

    protected bool CheckItem()
    {
        if (ammount <= 0) return false;
        ammount--;
        return true;
    }

    public int ammount;
    public float value;
    public bool doesExpire;
    public float duration;
}