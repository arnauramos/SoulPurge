using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraStamina : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.addExtraStamina(value);
        PlayerManager.Instance.addStamina(value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UseUpgrade);
    }
}
