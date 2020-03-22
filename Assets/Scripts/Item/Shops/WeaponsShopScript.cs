using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponsShopScript : MonoBehaviour
{
    // SHOP GAMEOBJECTS
    public List<GameObject> Slots;
    public TextMeshProUGUI Money;
    private SpriteRenderer Spriter;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI Price;
    // TEXT
    public TextMeshProUGUI Text;
    // ITEMS
    public List<Gun> WeaponsList;
    private Sprite auxSprite;

    void Start()
    {
        for (int i = 0; i < Slots.Capacity; i++)
        {
            // CHOOSE RANDOM WEAPON
            WeaponsList[i] = ItemsManager.Instance.GetRandomGun();
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
            Debug.Log(WeaponsList[i].price);
            Price.text = WeaponsList[i].price.ToString();
            // GET NAME COMPONENT
            Name = Slots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            // ADD ITEM NAME
            Name.text = WeaponsList[i].itemName.ToString();
        }

    }

    void Update()
    {
        if (InteractionManager.Instance.WeaponsShopping)
        {
            if (InteractionManager.Instance.OpenWeaponsShop)
            {
                //WELCOME MESSAGE
                StopAllCoroutines();
                StartCoroutine(sayWelcome());
                InteractionManager.Instance.OpenWeaponsShop = false;
            }
            for (int i = 0; i < Slots.Count; i++)
            {
                Debug.Log(i);
                // Get price
                Price = Slots[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                // Paint Prices
                Price.text = WeaponsList[i].price.ToString();
                // Update player money
                Money.text = PlayerManager.Instance.money.ToString();
            }
        }
    }

    public void BuyWeapon(int i)
    {
        if (PlayerManager.Instance.money >= WeaponsList[i].price)
        {
            // GIVE ITEMS
            int Bought = PlayerManager.Instance.addGun(WeaponsList[i]);
            switch (Bought)
            {
                case 0:
                    // BOUGHT
                    PlayerManager.Instance.money -= WeaponsList[i].price;
                    DataManager.Instance.MoneySpent += WeaponsList[i].price;
                    StopAllCoroutines();
                    StartCoroutine(sayBought(i));
                    break;
                case -1:
                    // DUPLICATED
                    StopAllCoroutines();
                    StartCoroutine(sayDuplicated(i));
                    break;
                case -2:
                    // NO SPACE
                    StopAllCoroutines();
                    StartCoroutine(sayNoSpace());
                    break;
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(sayNoMoney(i));
            // NO MONEY
        }
    }
    IEnumerator sayNoMoney(int i)
    {
        string say = "You don't have enough money to buy " + WeaponsList[i].itemName;
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayNoSpace()
    {
        string say = "You don't have enough space in your inventory";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayDuplicated(int i)
    {
        string say = "You already have "+ WeaponsList[i].itemName + " in your inventory";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayBought(int i)
    {
        string say = "You have bought " + WeaponsList[i].itemName + " for " + WeaponsList[i].price + " coins";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayWelcome()
    {
        string say = "Welcome to the Weapons Shop, how can I help you?";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
}
