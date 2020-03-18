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
    private Image BG;
    private SpriteRenderer Spriter;
    private Sprite auxSprite;
    private int Selected;

    // SLOTS AMMOUNTS
    public List<TextMeshProUGUI> SlotsAmmounts;
    private int auxAmmount;

    // COLORS
    private Color32 DarkYellow = new Color32(91, 81, 0, 255);
    private Color32 LightYellow = new Color32(188, 169, 0, 255);

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            // GETTING COMPONENTS AND DATA
            auxSprite = PlayerManager.Instance.PlayerUsableList[i].sprite;
            auxAmmount = PlayerManager.Instance.PlayerUsableList[i].ammount;
            Border = Slots[i].GetComponent<Image>();
            BG = Slots[i].transform.GetChild(0).GetComponent<Image>();
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
            // CHANGE BG COLOR IF SELECTED
            Selected = PlayerManager.Instance.usableSelected;
            if (i == Selected)
            {
                Border.color = Color.yellow;
                BG.color = LightYellow;
            }
            else
            {
                Border.color = Color.white;
                BG.color = DarkYellow;
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
