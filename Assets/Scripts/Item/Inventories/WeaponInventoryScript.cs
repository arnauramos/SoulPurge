using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInventoryScript : MonoBehaviour
{
    // SLOTS
    public List<GameObject> Slots;
    private Image Border;
    private Image BG;
    private SpriteRenderer Spriter;
    private Sprite auxSprite;
    private int Selected;

    // COLORS
    private Color32 DarkBlue = new Color32(0, 24, 91, 255);
    private Color32 LightBlue = new Color32(0, 50, 200, 255);
    void Update()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            // GETTING COMPONENTS AND DATA
            if (PlayerManager.Instance.PlayerGunList[i] != null)
            {
                auxSprite = PlayerManager.Instance.PlayerGunList[i].icon;
            }
            else
            {
                auxSprite = null;
            }
            
            Border = Slots[i].GetComponent<Image>();
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            BG = Slots[i].transform.GetChild(0).GetComponent<Image>();
            // RENDERING INVENTORY SPRITES
            if (auxSprite != null)
            {
                Spriter.sprite = auxSprite;
            }
            else
            {
                Spriter.sprite = null;
            }

            // CHANGE BORDER COLOR IF SELECTED
            // CHANGE BG COLOR IF SELECTED
            Selected = PlayerManager.Instance.weaponSelected;
            if (i == Selected)
            {
                Border.color = Color.cyan;
                BG.color = LightBlue;
            }
            else
            {
                Border.color = Color.white;
                BG.color = DarkBlue;
            }

        }

    }
}
