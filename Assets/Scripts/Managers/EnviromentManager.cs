using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour
{
	public static EnviromentManager Instance { get; private set; }

	public Transform RoofTilemapTransform;

	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Debug.Log("Error: Duplicated " + this + "in the scene");
		}
	}

	public void manageDoor(GameObject door)
	{
		Transform doorTransform = door.transform;
	
		if (doorTransform.eulerAngles == Vector3.forward * 0)
		{
			openDoor(doorTransform);
		}
		else if (doorTransform.eulerAngles == Vector3.forward * 90)
		{
			closeDoor(doorTransform);
		}
	}

	public void openDoor(Transform door)
	{
		if (Input.GetKeyDown(KeyCode.E) || PlayerManager.Instance.speed == PlayerManager.Instance.sprint)
		{
			door.eulerAngles = Vector3.forward * 90;
		}
	}
	public void closeDoor(Transform door)
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			door.eulerAngles = Vector3.forward * 0;
		}
	}

	public void openRoof()
	{
		RoofTilemapTransform = GameObject.Find("Roof-Tilemap").transform;
		RoofTilemapTransform.position = new Vector3(RoofTilemapTransform.position.x, RoofTilemapTransform.position.y, 1f);
	}
	public void closeRoof()
	{
		RoofTilemapTransform = GameObject.Find("Roof-Tilemap").transform;
		RoofTilemapTransform.position = new Vector3(RoofTilemapTransform.position.x, RoofTilemapTransform.position.y,-1f);
	}
}
