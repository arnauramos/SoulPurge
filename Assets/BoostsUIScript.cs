using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostsUIScript : MonoBehaviour
{
    public List<GameObject> BoostsIcons;
    private Color lerpColor = Color.cyan;

    void Update()
    {
        CheckBoosts();
        ColorLerp();
        ChangeColors();
    }

    private void CheckBoosts()
    {
        if (PlayerManager.Instance.resistanceActive)
        {
            BoostsIcons[0].SetActive(true);
        }
        else
        {
            BoostsIcons[0].SetActive(false);
        }
        if (PlayerManager.Instance.staminaRegenerationActive)
        {
            BoostsIcons[1].SetActive(true);
        }
        else
        {
            BoostsIcons[1].SetActive(false);
        }
        if (PlayerManager.Instance.speedBoostActive)
        {
            BoostsIcons[2].SetActive(true);
        }
        else
        {
            BoostsIcons[2].SetActive(false);
        }
        if (PlayerManager.Instance.shootingBoostActive)
        {
            BoostsIcons[3].SetActive(true);
        }
        else
        {
            BoostsIcons[3].SetActive(false);
        }
    }
    private void ColorLerp()
    {
        lerpColor = Color.Lerp(Color.cyan, Color.white, Mathf.PingPong(Time.time, 2));
    }
    private void ChangeColors()
    {
        for (int i = 0; i < BoostsIcons.Capacity; i++)
        {
            BoostsIcons[i].GetComponent<SpriteRenderer>().color = lerpColor;
        }
    }
}
