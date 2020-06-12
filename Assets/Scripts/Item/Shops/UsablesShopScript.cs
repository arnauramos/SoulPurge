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
    // TEXT
    public TextMeshProUGUI Text;
    // ITEMS
    public List<Usable> ItemsList;
    public List<int> Ammounts;
    public List<int> Prices;
    private Sprite auxSprite;

    void Start()
    {
        bool GoodItem;
        for (int i = 0; i < Slots.Capacity; i++)
        {
            do
            {
                GoodItem = true;
                // CHOOSE RANDOM ITEM
                ItemsList[i] = ItemsManager.Instance.GetRandomUsable();
                // CHECK SHOP ITEMS
                for (int x = 0; x < Slots.Capacity; x++)
                {
                    if (ItemsList[x] == null)
                    {
                        continue;
                    }
                    if (ItemsList[i].itemName == ItemsList[x].itemName && i != x)
                    {
                        GoodItem = false;
                    }
                }
            } while (!GoodItem);

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
            if (InteractionManager.Instance.OpenUsablesShop)
            {
                //WELCOME MESSAGE
                StopAllCoroutines();
                StartCoroutine(sayWelcome());
                InteractionManager.Instance.OpenUsablesShop = false;
            }
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
        // CHECK IF AMMOUNT TO BUY IS > 0
        if (Ammounts[i] > 0)
        {
            if (PlayerManager.Instance.money >= Prices[i])
            {
                // GIVE ITEMS
                bool Bought = PlayerManager.Instance.addUsable(ItemsList[i], Ammounts[i]);
                if (Bought)
                {
                    // DECREASE MONEY
                    SoundManager.Instance.PlaySound(SoundManager.Sounds.Shop);
                    PlayerManager.Instance.money -= Prices[i];
                    DataManager.Instance.MoneySpent += Prices[i];
                    StopAllCoroutines();
                    StartCoroutine(sayBought(i));
                }
                else
                {
                    // NO SPACE
                    StopAllCoroutines();
                    StartCoroutine(sayNoSpace());
                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(sayNoMoney(i));
                // NO MONEY
            }
        }
    }
    IEnumerator sayNoMoney(int i)
    {
        string n = ItemsList[i].itemName;
        if (Ammounts[i] > 1)
        {
            n += "s";
        }
        string say = "You don't have enough money to buy " + Ammounts[i] + " " + n;
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
    IEnumerator sayBought(int i)
    {
        string n = ItemsList[i].itemName;
        if (Ammounts[i] > 1)
        {
            n += "s";
        }
        string say = "You have bought " + Ammounts[i] + " " + n +" for "+ Prices[i]  + " coins";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayWelcome()
    {
        string say = "Welcome to the Items Shop, how can I help you?";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
}
