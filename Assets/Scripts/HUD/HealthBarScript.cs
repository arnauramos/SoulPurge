using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    Vector2 healthSize = new Vector2(1f, 1f);
    float healthFloat = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSize.x > .001f)
        {
            healthFloat = PlayerManager.Instance.health / PlayerManager.Instance.maxHeath;
            if (healthFloat < 0) healthFloat = 0;
            healthSize.x = healthFloat;
            transform.localScale = healthSize;
        }
    }
}
