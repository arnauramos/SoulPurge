using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceBoost : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.UseResistance(duration, value);
    }
}
