using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Item
{
    public abstract void Use();

    protected abstract void CheckItem(); 

    public int ammount;
    public float value;
    public bool doesExpire;
    public float duration;
}