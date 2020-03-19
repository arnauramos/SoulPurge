using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsablesShopScript : MonoBehaviour
{
    // SHOP GAMEOBJECTS
    public List<GameObject> Slots;
    public TextMeshProUGUI Money;
    private SpriteRenderer Spriter;
    private TextMeshProUGUI Name;
    private TextMeshProUGUI Price;
    private TextMeshProUGUI Ammount;
    // ITEMS
    public List<Usable> ItemsList;
    public List<int> Ammounts;
    public List<int> Prices;
    private Sprite auxSprite;

    void Start()
    {   
        for (int i = 0; i < Slots.Capacity; i++)
        {
            // CHOOSE RANDOM ITEM
            ItemsList[i] = ItemsManager.Instance.GetRandomUsable();
            // GET ITEM SPRITE
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            auxSprite = ItemsList[i].sprite;
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
            Price = Slots[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>(); 
            Prices.Add(ItemsList[i].price);
            // ADD ITEM PRICE
            Price.text = Prices[i].ToString();
            // GET NAME COMPONENT
            Name = Slots[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            // ADD ITEM NAME
            Name.text = ItemsList[i].itemName.ToString();
            // ADD AMMOUNT
            Ammounts.Add(1);
        }
    }

    void Update()
    {
        if (InteractionManager.Instance.UsablesShopping)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                // Get Ammount
                Ammount = Slots[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                // Paint ammounts
                Ammount.text = Ammounts[i].ToString();
                // Get price
                Price = Slots[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
                // Paint Prices
                Prices[i] = ItemsList[i].price * Ammounts[i];
                Price.text = Prices[i].ToString();
                // Update player money
                Money.text = PlayerManager.Instance.money.ToString();
            }
        }
    }

    public void IncrementAmmount(int i)
    {
        // Increment ammount by 1
        Ammounts[i] += 1;
        if (Ammounts[i] > 99)
        {
            Ammounts[i] = 99;
        }
    }
    public void DecrementAmmount(int i)
    {
        // Decrement ammount by 1
        Ammounts[i] -= 1;
        if (Ammounts[i] < 0)
        {
            Ammounts[i] = 0;
        }
    }

    public void BuyItem(int i)
    {
        if (PlayerManager.Instance.money >= Prices[i])
        {
            // GIVE ITEMS
            bool Bought = PlayerManager.Instance.addUsable(ItemsList[i], Ammounts[i]);
            if (Bought)
            {
                // DECREASE MONEY
                PlayerManager.Instance.money -= Prices[i];
            }
            else
            {
                // NO SPACE
            }
        }
        else
        {
            // NO MONEY
        }
    }


}
