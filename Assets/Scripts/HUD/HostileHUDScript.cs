using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HostileHUDScript : MonoBehaviour
{
    public TextMeshProUGUI WaveTitle;
    public TextMeshProUGUI EnemiesTitle;
    public TextMeshProUGUI WaveCounter;
    public TextMeshProUGUI EnemiesCounter;
    public TextMeshProUGUI NextSafeZone;
    private int nextWave = 0;
    private bool changedNextWave = false;
    // Update is called once per frame
    private void Start()
    {
        nextWave = SpawnerManager.Instance.ActualRound;
    }
    void Update()
    {
        if (SpawnerManager.Instance.TotalEnemies <= 0)
        {
            if (!changedNextWave)
            {
                nextWave += 1;
                changedNextWave = true;
            }
            EnemiesTitle.enabled = false;
            EnemiesCounter.enabled = false;
            WaveTitle.text = "NEXT WAVE: " + nextWave.ToString();
            WaveCounter.enabled = false;
        }
        else
        {
            changedNextWave = false;
            WaveCounter.text = (SpawnerManager.Instance.ActualRound + 1).ToString();
            EnemiesCounter.text = SpawnerManager.Instance.TotalEnemies.ToString();
            EnemiesTitle.enabled = true;
            EnemiesCounter.enabled = true;
            WaveTitle.text = "CURRENT WAVE: ";
            WaveCounter.enabled = true;
        }
        if (PlayerManager.Instance.keys >= 3)
        {
            NextSafeZone.text = "You can go to the next safezone";
        }
        else
        {
            NextSafeZone.text = " ";
        }
    }
}
