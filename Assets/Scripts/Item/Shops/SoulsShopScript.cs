using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulsShopScript : MonoBehaviour
{
    public int SoulsPrice;
    private int SoulsAmmount;
    private int TotalPrice;
    // SHOP SOULS
    public List<GameObject> Slots;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Souls;
    private TextMeshProUGUI Ammount;
    private TextMeshProUGUI Price;
    // TEXT
    public TextMeshProUGUI Text;


    void Start()
    {
        // Set price
        SoulsPrice = Random.Range(2, 7);
        // Get price
        Price = Slots[0].transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        // Paint Price
        TotalPrice = SoulsPrice * SoulsAmmount;
        Price.text = TotalPrice.ToString();
        // Get ammount
        Ammount = Slots[0].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        // Paint ammount
        Ammount.text = SoulsAmmount.ToString();
        // Check souls
        if (SoulsAmmount > PlayerManager.Instance.souls)
        {
            SoulsAmmount = PlayerManager.Instance.souls;
        }
    }

    void Update()
    {
        if (InteractionManager.Instance.SoulsShopping)
        {
            if (InteractionManager.Instance.OpenSoulsShop)
            {
                //WELCOME MESSAGE
                StopAllCoroutines();
                StartCoroutine(sayWelcome());
                InteractionManager.Instance.OpenSoulsShop = false;
            }
            // Update player money
            Money.text = PlayerManager.Instance.money.ToString();
            // Update player souls
            Souls.text = PlayerManager.Instance.souls.ToString();
            // Update ammount
            Ammount.text = SoulsAmmount.ToString();
            // Update price
            TotalPrice = SoulsPrice * SoulsAmmount;
            Price.text = TotalPrice.ToString();
        }
    }

    public void SellSouls()
    {
        if (PlayerManager.Instance.souls >= SoulsAmmount)
        {
            // SELL
            InteractionManager.Instance.SoulsExchange(SoulsAmmount, SoulsPrice);
            // SOLD
            StopAllCoroutines();
            StartCoroutine(saySold());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(sayNoSouls());
            // NO SOULS
        }
    }

    public void IncrementAmmount(int i)
    {
        // Increment ammount by i
        SoulsAmmount += i;
        if (SoulsAmmount > PlayerManager.Instance.souls)
        {
            SoulsAmmount = PlayerManager.Instance.souls;
        }
    }
    public void DecrementAmmount(int i)
    {
        // Decrement ammount by i
        SoulsAmmount -= i;
        if (SoulsAmmount < 0)
        {
            SoulsAmmount = 0;
        }
    }
    public void SetAmmountAll()
    {
        // Set ammount all
        SoulsAmmount = PlayerManager.Instance.souls;
    }
    public void SetAmmountNone()
    {
        // Set ammount none
        SoulsAmmount = 0;
    }

    IEnumerator sayNoSouls()
    {
        string say = "You don't have " + SoulsAmmount + " soul";
        if (SoulsAmmount > 1)
        {
            say += "s";
        }
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator saySold()
    {
        string soldSaid = "You have sold " + SoulsAmmount + " soul";
        if (SoulsAmmount > 1)
        {
            soldSaid += "s";
        }
        string say = soldSaid + " for " + SoulsPrice * SoulsAmmount + " coins";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
    IEnumerator sayWelcome()
    {
        string say = "Welcome to the Souls Shop, how can I help you?";
        Text.text = " ";
        for (int x = 0; x < say.Length; x++)
        {
            Text.text += say[x];
            yield return null;
        }
    }
}
