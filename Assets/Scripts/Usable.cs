using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Item
{
    public abstract void Use();
    public int ammount;
    public bool doesExpire;
    public float duration;
}