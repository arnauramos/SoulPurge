using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsablesSellScript : MonoBehaviour
{
    // SHOP GAMEOBJECTS
    public List<GameObject> Slots;
    public List<GameObject> Buttons;
    public List<TextMeshProUGUI> CurrentAmmounts;
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

    void Start()
    {
        InitInventory();
        UpdateInventory();
    }

    void Update()
    {
        if (InteractionManager.Instance.UsablesShopping)
        {
            UpdateInventory();
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
        if (Ammounts[i] > PlayerManager.Instance.PlayerUsableList[i].ammount)
        {
            Ammounts[i] = PlayerManager.Instance.PlayerUsableList[i].ammount;
        }
    }
    public void DecrementAmmount(int i)
    {
        // Decrement ammount by 1
        Ammounts[i] -= 1;
        if (Ammounts[i] <= 0)
        {
            Ammounts[i] = 1;
        }
    }

    public void SellItem(int i)
    {
        // CHECK IF AMMOUNT TO BUY IS > 0
        if (Ammounts[i] > 0)
        {
            if (Ammounts[i] <= PlayerManager.Instance.PlayerUsableList[i].ammount)
            {
                // SAY SOLD
                StopAllCoroutines();
                StartCoroutine(saySold(i));
                // REMOVE ITEM
                int price = Prices[i];
                PlayerManager.Instance.removeUsable(i, Ammounts[i]);
                // GIVE MONEY
                PlayerManager.Instance.addMoney(price);
            }
            else
            {
                // NOT ENOUGH ITEMS TO SELL
            }
        }
    }
    private void UpdateInventory()
    {
        ItemsList = PlayerManager.Instance.PlayerUsableList;
        // FOR PLAYER USABLES
        for (int i = 0; i < ItemsList.Capacity; i++)
        {
            // GET COMPONENTS
            Name = Slots[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            Price = Slots[i].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
            Spriter = Slots[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            Ammount = Slots[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            // IF NULL
            if (ItemsList[i] ==  null || ItemsList[i].ammount <= 0)
            {
                // NAME = ""
                Name.text = " ";
                // PRICE = 0
                Price.text = "0";
                // AMMOUNT = 0
                Ammount.text = "0";
                // SPRITE = NULL
                Spriter.sprite = null;
                // SELL BUTTON = FALSE
                Buttons[i].SetActive(false);
                // CURRENT AMMOUNT = " "
                CurrentAmmounts[i].text = " ";
            }
            // IF NOT NULL
            else
            {
                // NAME = ITEM NAME
                Name.text = ItemsList[i].itemName;
                // AMMOUNT = AMMOUNT SELECTED;
                if (Ammounts[i] <= 0)
                {
                    Ammounts[i] = 1;
                }
                else if (Ammounts[i] > ItemsList[i].ammount)
                {
                    Ammounts[i] = ItemsList[i].ammount;
                }
                Ammount.text = Ammounts[i].ToString();
                // PRICE = ITEM PRICE * AMMOUNT SELECTED
                Prices[i] = (int)(ItemsList[i].price * Ammounts[i] * 0.95f);
                Price.text = Prices[i].ToString();
                // SPRITE = ITEM SPRITE
                Spriter.sprite = ItemsList[i].sprite;
                // SELL BUTTON = TRUE
                Buttons[i].SetActive(true);
                // CURRENT AMMOUNT
                CurrentAmmounts[i].text = ItemsList[i].ammount.ToString();
            }
        }
    }
    private void InitInventory()
    {
        int capacity = PlayerManager.Instance.PlayerUsableList.Capacity;
        // INIT AMMOUNTS
        for (int i = 0; i < capacity; i++)
        {
            Ammounts.Add(0);
        }
        // INIT PRICES
        for (int i = 0; i < capacity; i++)
        {
            Prices.Add(0);
        }
        // INIT ITEMS LIST
        for (int i = 0; i < capacity; i++)
        {
            ItemsList.Add(null);
        }
    }
    IEnumerator saySold(int i)
    {
        string n = ItemsList[i].itemName;
        if (Ammounts[i] > 1)
        {
            n += "s";
        }
        string say = "You have sold " + Ammounts[i] + " " + n + " for "+  Prices[i] +" coins.";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }

}
