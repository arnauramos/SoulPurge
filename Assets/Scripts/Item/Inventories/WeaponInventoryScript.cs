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
    private SpriteRenderer Spriter;
    private Sprite auxSprite;
    private int Selected;

    void Update()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            // GETTING COMPONENTS AND DATA
            if (PlayerManager.Instance.PlayerGunList[i] != null)
            {
                auxSprite = PlayerManager.Instance.PlayerGunList[i].sprite;
            }
            else
            {
                auxSprite = null;
            }
            
            Border = Slots[i].GetComponent<Image>();
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

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
            Selected = PlayerManager.Instance.weaponSelected;
            if (i == Selected)
            {
                Border.color = Color.cyan;
            }
            else
            {
                Border.color = Color.white;
            }
        }

    }
}
