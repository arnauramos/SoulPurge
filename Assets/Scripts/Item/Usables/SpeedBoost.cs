using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.UseSpeedBoost(duration, value);
    }
}
