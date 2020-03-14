using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    [Header("Map Bounties:")]
    public Vector2 TopLeftLimit;
    public Vector2 BotRightLimit;

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

        TopLeftLimit = GameObject.Find("Top-Left").transform.position;
        BotRightLimit = GameObject.Find("Bot-Right").transform.position;
    }

    private Vector2 GenerateRawPosition()
    {
        return new Vector2(Random.Range(TopLeftLimit.x, BotRightLimit.x), Random.Range(BotRightLimit.y, TopLeftLimit.y));
    }

    public Vector2 CheckRawPosition()
    {
        Vector2 RawPosition = GenerateRawPosition();

        if ((RawPosition.x >= TopLeftLimit.x / 2 && RawPosition.x <= BotRightLimit.x / 2) && (RawPosition.y >= BotRightLimit.y / 2 && RawPosition.y <= TopLeftLimit.y / 2))
        {
            RawPosition = CheckRawPosition();
        }

        NetPosition = RawPosition;

        //  Enemy Randomize
        int EnemyToSpawn = Random.Range(0, 2);
        if (EnemyToSpawn == 0) Instantiate(AuxEnemy, NetPosition, Quaternion.identity);
        else Instantiate(AuxEnemyShooter, NetPosition, Quaternion.identity);

        return NetPosition;
    }
}
