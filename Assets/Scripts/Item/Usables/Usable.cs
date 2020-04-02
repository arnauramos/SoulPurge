using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : Item
{
    public abstract void Use();

    protected bool CheckItem()
    {
        if (ammount <= 0) return false;
        // DELETE FROM INVENTORY
        for (int i = 0; i < PlayerManager.Instance.PlayerUsableList.Capacity; i++)
        {
            if (this == PlayerManager.Instance.PlayerUsableList[i])
            {
                PlayerManager.Instance.removeUsable(i, 1);
                return true;
            }
        }
        return false;
    }

    public int ammount;
    public float value;
    public bool doesExpire;
    public float duration;
}