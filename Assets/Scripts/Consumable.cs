using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Consumable/ConsumableScriptableObject", order = 1)]
public class Consumable : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float Value;
    public float Duration;
}