﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarScript : MonoBehaviour
{
    Vector2 staminaSize = new Vector2(1f, 1f);
    float staminaFloat = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaSize.x > .001f)
        {
            staminaFloat = PlayerManager.Instance.stamina / PlayerManager.Instance.maxStamina;
            if (staminaFloat < 0) staminaFloat = 0;
            staminaSize.x = staminaFloat;
            transform.localScale = staminaSize;
        }
        else
        {
            staminaFloat = PlayerManager.Instance.stamina / PlayerManager.Instance.maxStamina;
            staminaSize.x = staminaFloat;
            transform.localScale = staminaSize;
        }
    }
}
