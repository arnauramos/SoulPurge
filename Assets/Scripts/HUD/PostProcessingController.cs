using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;

    private float intensity = 0f;

    private float lastHp = 100f;
    public float effectTimer = 0f;
    private bool damageEffect = false;
    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
        intensity = 0f;
        vignette.intensity.value = intensity;
        lastHp = PlayerManager.Instance.health;
    }

    void Update()
    {
        if (PlayerManager.Instance.health < lastHp)
        {
            effectTimer = 0f;
            damageEffect = true;
        }
        lastHp = PlayerManager.Instance.health;

        if (damageEffect)
        {
            DamageEffect();
        }
    }
    private void DamageEffect()
    {
        if (effectTimer >= 0.5f)
        {
            intensity -= Time.deltaTime;
            if (intensity <= 0f)
            {
                intensity = 0f;
                effectTimer = 0f;
                damageEffect = false;
            }
        }
        else
        {
            effectTimer += Time.deltaTime;
            intensity += (Time.deltaTime * 2);
            if (intensity > 0.55f)
            {
                intensity = 0.55f;
            }
        }
        vignette.intensity.value = intensity;

    }
}
