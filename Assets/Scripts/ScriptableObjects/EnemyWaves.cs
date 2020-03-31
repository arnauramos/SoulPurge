using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyWaves", order = 1)]
public class EnemyWaves : ScriptableObject
{
    public int MaxRounds;
    public Vector2Int[] EnemyTypePerRound; // 0 = Melee / 1 = Ranged
}
