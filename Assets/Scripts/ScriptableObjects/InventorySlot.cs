using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InventorySlot", order = 1)]
public class InventorySlot : ScriptableObject
{
    public string Name;
    public GameObject InventaryObject;
    public int Quantity;
}
