using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    public bool START;

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
        //  Get Map Bounties
        MapTopLeftLimit = GameObject.Find("Top-Left").transform.position;
        MapBotRightLimit = GameObject.Find("Bot-Right").transform.position;
        //  Get Camera Bounties
        CameraTopLeftLimit = new Vector2 (Camera.main.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize) * 1.5f;
        CameraBotRightLimit = new Vector2(Camera.main.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize) * 1.5f;
        //CameraTopLeftLimit = new Vector2(-12,12);
        //CameraBotRightLimit = new Vector2(12,-12);
    }

    private void Update()
    {
        CameraTopLeftLimit = new Vector2(Camera.main.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize) * 1.5f;
        CameraBotRightLimit = new Vector2(Camera.main.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize) * 1.5f;
    }

    private Vector2 GenerateRawPosition()
    {
        return new Vector2(Random.Range(MapTopLeftLimit.x, MapBotRightLimit.x), Random.Range(MapBotRightLimit.y, MapTopLeftLimit.y));
    }

    public Vector2 CheckRawPosition()
    {
        Vector2 RawPosition = GenerateRawPosition();

        if (!START) return RawPosition;

        if ((RawPosition.x >= CameraTopLeftLimit.x && RawPosition.x <= CameraBotRightLimit.x ) && (RawPosition.y >= CameraBotRightLimit.y && RawPosition.y <= CameraTopLeftLimit.y))
        {
            RawPosition = CheckRawPosition();
        }

        NetPosition = RawPosition;

        //  Enemy Randomize
        int EnemyToSpawn = Random.Range(0, 1);
        if (EnemyToSpawn == 0) Instantiate(AuxEnemy, NetPosition, Quaternion.identity);
        else Instantiate(AuxEnemyShooter, NetPosition, Quaternion.identity);

        return NetPosition;
    }
}
