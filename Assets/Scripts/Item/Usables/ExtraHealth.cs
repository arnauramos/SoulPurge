using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.addExtraHealth(value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UseUpgrade);
    }
}
