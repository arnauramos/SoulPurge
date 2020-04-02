using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    public bool STOP;

    [Header("Map Bounties:")]
    public Vector2 MapTopLeftLimit;
    public Vector2 MapBotRightLimit;

    [Header("Camera Bounties:")]
    public Vector2 CameraTopLeftLimit;
    public Vector2 CameraBotRightLimit;

    [Header("Final Spawn Position:")]
    public Vector2 NetPosition;
    private Vector2 RawPosition;

    [Header("Enemies to spawn:")]
    public Enemy AuxEnemy;
    public EnemyShooter AuxEnemyShooter;

    [Header("Enemy Waves:")]
    public int ActualRound = 0;
    bool RoundSpawnComplete;
    public Vector2Int[] EnemyTypePerRound;
    public EnemyWaves HostileZoneWaves;

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
    //EnemyTypePerRound = HostileZoneWaves.EnemyTypePerRound;
    //AuxActualRound = ActualRound;
}

    private void Update()
    {
        if (!PlayerSceneManager.Instance.ZoneIsHostile)
        {
            ActualRound = 0;
            return;
        }
        MapTopLeftLimit = new Vector2(GameObject.Find("West_Wall").transform.position.x, GameObject.Find("North_Wall").transform.position.y);
        MapBotRightLimit = new Vector2(GameObject.Find("East_Wall").transform.position.x, GameObject.Find("South_Wall").transform.position.y);


        CameraTopLeftLimit = new Vector2(Camera.main.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize);
        CameraBotRightLimit = new Vector2(Camera.main.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize);
    }
    private Vector2 GenerateRawPosition()
    {
        return new Vector2(Random.Range(MapTopLeftLimit.x, MapBotRightLimit.x), Random.Range(MapBotRightLimit.y, MapTopLeftLimit.y));
    }

    public Vector2 CheckRawPosition()
    {
        Vector2 RawPosition = GenerateRawPosition();

        if (STOP) return RawPosition;

        if ((RawPosition.x >= CameraTopLeftLimit.x && RawPosition.x <= CameraBotRightLimit.x ) && (RawPosition.y >= CameraBotRightLimit.y && RawPosition.y <= CameraTopLeftLimit.y))
        {
            RawPosition = CheckRawPosition();
        }

        NetPosition = RawPosition;

        //  Enemy Randomize
        //int EnemyToSpawn = Random.Range(0, 2);
        //if (EnemyToSpawn == 0) Instantiate(AuxEnemy, NetPosition, Quaternion.identity);
        //else Instantiate(AuxEnemyShooter, NetPosition, Quaternion.identity);

        //Debug.Log(NetPosition);
        return NetPosition;
    }

    public void Spawner()
    {
        if (RoundSpawnComplete || !PlayerSceneManager.Instance.ZoneIsHostile) return;

        // Enemy Spawn
        int EnemiesToSpawn = HostileZoneWaves.EnemyTypePerRound[ActualRound].x;
        for (int m = 0; m < EnemiesToSpawn; m++)
        {
            NetPosition = CheckRawPosition();
            Instantiate(AuxEnemy, NetPosition, Quaternion.identity);
        }
        EnemiesToSpawn = HostileZoneWaves.EnemyTypePerRound[ActualRound].y;
        for (int r = 0; r < EnemiesToSpawn; r++)
        {
            NetPosition = CheckRawPosition();
            Instantiate(AuxEnemyShooter, NetPosition, Quaternion.identity);
        }

        RoundSpawnComplete = true;
    }

    public void EnemyChecker()
    {
        int MeleeEnemies = GameObject.FindObjectsOfType<Enemy>().Length;
        int RangedEnemies = GameObject.FindObjectsOfType<EnemyShooter>().Length;

        if ((MeleeEnemies == 0 && RangedEnemies == 0) && PlayerSceneManager.Instance.ZoneIsHostile)
        {
            ActualRound++;
            RoundSpawnComplete = false;
        }
    }
}
