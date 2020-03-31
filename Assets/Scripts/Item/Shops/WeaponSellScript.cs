using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSellScript : MonoBehaviour
{
    // SHOP GAMEOBJECTS
    public List<GameObject> Slots;
    public List<GameObject> Buttons;
    private TextMeshProUGUI Money;
    private SpriteRenderer Spriter;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI Price;
    // TEXT
    public TextMeshProUGUI Text;
    // INVENTORY
    public List<Gun> WeaponsList;
    private Sprite auxSprite;
    // Start is called before the first frame update
    void Start()
    {
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventory();
    }
    private void UpdateInventory()
    {
        WeaponsList = PlayerManager.Instance.PlayerGunList;
        for (int i = 0; i < WeaponsList.Capacity; i++)
        {
            if (WeaponsList[i] == null)
            {
                Price = Slots[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                Price.text = "0";
                Name = Slots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                Name.text = " ";
                Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                Spriter.sprite = null;
                Buttons[i].SetActive(false);
                continue;
            }
            Buttons[i].SetActive(true);
            // GET ITEM SPRITE
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            auxSprite = WeaponsList[i].sprite;
            // ADD SPRITE TO SPRITER
            if (auxSprite != null)
            {
                Spriter.sprite = auxSprite;
            }
            else
            {
                Spriter.sprite = null;
            }
            // GET ITEM PRICE
            Price = Slots[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            // ADD ITEM PRICE
            Price.text = WeaponsList[i].price.ToString();
            // GET NAME COMPONENT
            Name = Slots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            // ADD ITEM NAME
            Name.text = WeaponsList[i].itemName.ToString();
        }
    }
    public void SellWeapon(int i)
    {
        if (PlayerManager.Instance.PlayerGunList[i] != null)
        {
            // SAY SOLD
            StopAllCoroutines();
            StartCoroutine(saySold(i));
            // REMOVE ITEM
            int price = PlayerManager.Instance.PlayerGunList[i].price;
            PlayerManager.Instance.removeGun(i);
            // GIVE MONEY
            PlayerManager.Instance.addMoney(price);
        }
    }
    IEnumerator saySold(int i)
    {
        string say = "You have sold " + WeaponsList[i].itemName + " for " + WeaponsList[i].price + " coins";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
}
