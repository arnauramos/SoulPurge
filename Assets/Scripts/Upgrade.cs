using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Upgrade/UpgradeScriptableObject", order = 1)]
public class Upgrade : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float Value;
    public bool Permanent;
    public float Duration;
}