using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private GameObject North;
    private GameObject South;
    private GameObject East;
    private GameObject West;
    public Camera MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        North = GameObject.Find("North_Wall");
        South = GameObject.Find("South_Wall");
        East = GameObject.Find("East_Wall");
        West = GameObject.Find("West_Wall");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, West.transform.position.x + (MainCamera.orthographicSize * MainCamera.aspect), East.transform.position.x - (MainCamera.orthographicSize * MainCamera.aspect)),
            Mathf.Clamp(player.transform.position.y, South.transform.position.y + MainCamera.orthographicSize, North.transform.position.y - MainCamera.orthographicSize), 
            0);
        //CheckBorders();
    }
    //void CheckBorders()
    //{
    //    if (transform.position.y+1.7 >= North.transform.position.y)
    //    {
    //    }
    //    else
    //    {
    //        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    //    }
    //}
}
