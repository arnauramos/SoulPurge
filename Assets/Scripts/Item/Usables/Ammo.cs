using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.addTotalAmmo((int)value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UseAmmo);
    }
}
