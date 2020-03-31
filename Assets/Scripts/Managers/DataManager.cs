using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // DATA
    public int BulletsShot;
    public int ObtainedSouls;
    public int MoneySpent;
    public float DistanceTravelled = 0;


    // AUX DATA
    private Vector2 OldPosition;
    private Vector2 PositionsDif;
    private float NewDistanceTravelled;

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
    public void TrackDistance(Vector2 NewPosition) {
        if (DistanceTravelled == 0)
        {
            OldPosition = NewPosition;
            DistanceTravelled = 0.00001f;
        }
        PositionsDif.x = Mathf.Abs(OldPosition.x - NewPosition.x);
        PositionsDif.y = Mathf.Abs(OldPosition.y - NewPosition.y);
        NewDistanceTravelled = Mathf.Sqrt(Mathf.Pow(PositionsDif.x, 2) + Mathf.Pow(PositionsDif.y, 2));
        DistanceTravelled += NewDistanceTravelled;
        OldPosition = NewPosition;
    }

    // RESET VARIABLES

    public void reset()
    {
    // DATA
    BulletsShot = 0;
    ObtainedSouls = 0;
    MoneySpent = 0;
    DistanceTravelled = 0;
    }
}