using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationBoost : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.UseStaminaRegeneration(duration, value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UseUpgrade);
    }
}
