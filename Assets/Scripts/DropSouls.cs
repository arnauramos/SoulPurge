using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSouls : MonoBehaviour
{
    public int manolo;
    public void DropingSouls(GameObject _enemy, GameObject _soul)
    {
        float RandX, RandY;
        for (int i = 1; i < 4; i++)
        {
            RandX = Random.Range(-0.15f, 0.15f);
            RandY = Random.Range(-0.15f, 0.15f);
            Instantiate(_soul, new Vector3(_enemy.transform.position.x + RandX, _enemy.transform.position.y + RandY), _enemy.transform.rotation);
        }
    }
}
