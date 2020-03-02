using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public float health;
    public float maxHeath;
    public float resistance;
    public float stamina;
    public float maxStamina;
    public float staminaRegeneration;
    public float speed;
    public float speedBoost;
    public int souls;
    public int maxSouls;
    public int money;
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
}
