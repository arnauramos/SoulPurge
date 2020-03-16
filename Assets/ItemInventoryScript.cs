using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryScript : MonoBehaviour
{
    public List<GameObject> Slots;
    private Sprite auxSprite;
    private int auxAmmount;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            auxSprite = PlayerManager.Instance.PlayerUsableList[i].sprite;
            auxAmmount = PlayerManager.Instance.PlayerUsableList[i].ammount;
            if (auxSprite != null && auxAmmount > 0)
            {
                Slots[i].GetComponent<SpriteRenderer>().sprite = auxSprite;
            }
            else
            {
                Slots[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
}
