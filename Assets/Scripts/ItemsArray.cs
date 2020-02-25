using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsArray: MonoBehaviour
{
	public InventorySlot[] ArrItemSlot;

	public static InventorySlot[] ArrayItemSlot;

	public void Awake()
	{
		ArrayItemSlot = ArrItemSlot;
	}
}
