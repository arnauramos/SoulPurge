using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsExchange : MonoBehaviour
{
    //public Canvas hola;
    private int moneyToGive;
    public int Exchange(int _souls)
    {
        moneyToGive = _souls * 10;
        _souls = 0;
        return moneyToGive;
    }
}
