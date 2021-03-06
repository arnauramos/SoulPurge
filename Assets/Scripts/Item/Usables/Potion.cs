﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Usable
{
    public override void Use()
    {
        if (!CheckItem()) return;
        PlayerManager.Instance.addHealth(value);
        SoundManager.Instance.PlaySound(SoundManager.Sounds.UsePotion);
    }
}
