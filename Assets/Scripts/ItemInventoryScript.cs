using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInventoryScript : MonoBehaviour
{
    // SLOTS
    public List<GameObject> Slots;
    private Image Border;
    private SpriteRenderer Spriter;
    private Sprite auxSprite;
    private int Selected;

    // SLOTS AMMOUNTS
    public List<TextMeshProUGUI> SlotsAmmounts;
    private int auxAmmount;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            // GETTING COMPONENTS AND DATA
            auxSprite = PlayerManager.Instance.PlayerUsableList[i].sprite;
            auxAmmount = PlayerManager.Instance.PlayerUsableList[i].ammount;
            Border = Slots[i].GetComponent<Image>();
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

            // RENDERING INVENTORY SPRITES
            if (auxSprite != null && auxAmmount > 0)
            {
                Spriter.sprite = auxSprite;
            }
            else
            {
                Spriter.sprite = null;
            }

            // CHANGE BORDER COLOR IF SELECTED
            Selected = PlayerManager.Instance.usableSelected;
            if (i == Selected)
            {
                Border.color = Color.yellow;
            }
            else
            {
                Border.color = Color.white;
            }

            // CHANGE SLOTS AMMOUNTS
            if (auxAmmount > 0)
            {
                SlotsAmmounts[i].text = auxAmmount.ToString();
                SlotsAmmounts[i].enabled = true;
            }
            else
            {
                SlotsAmmounts[i].text = "0";
                SlotsAmmounts[i].enabled = false;
            }
        }

    }
}
