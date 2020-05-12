using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoost : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.UseShootingBoost(duration, value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UseUpgrade);
    }
}
