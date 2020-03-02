using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsArray: MonoBehaviour
{
	public Usable[] ArrItemSlot;

	public static Usable[] ArrayItemSlot;

	public void Awake()
	{
		ArrayItemSlot = ArrItemSlot;
	}
}
