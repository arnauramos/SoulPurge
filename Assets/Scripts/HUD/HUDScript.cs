using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    // STAMINA
    public GameObject StaminaBar;
    Vector2 staminaSize = new Vector2(1f, 1f);
    float staminaFloat = 1f;

    // HEALTH
    public GameObject HealthBar;
    Vector2 healthSize = new Vector2(1f, 1f);
    float healthFloat = 1f;

    // SOULS
    public GameObject SoulsCounter;
    private TextMeshProUGUI SoulsText;

    // MONEY
    public GameObject MoneyCounter;
    private TextMeshProUGUI MoneyText;

    // AMMO
    public GameObject RoundsCounter;
    private TextMeshProUGUI RoundsText;
    public GameObject TotalAmmoCounter;
    private TextMeshProUGUI TotalAmmoText;

    // PLAYER
    private GameObject player;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        // SOULS
        SoulsText = SoulsCounter.GetComponent<TextMeshProUGUI>();

        // MONEY
        MoneyText = MoneyCounter.GetComponent<TextMeshProUGUI>();

        // AMMO
        RoundsText = RoundsCounter.GetComponent<TextMeshProUGUI>();
        TotalAmmoText = TotalAmmoCounter.GetComponent<TextMeshProUGUI>();

        // PLAYER
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Stamina();
        Health();
        Souls();
        Money();
        Ammo();
    }
    private void Stamina() {
        if (staminaSize.x > .001f)
        {
            staminaFloat = PlayerManager.Instance.stamina / PlayerManager.Instance.maxStamina;
            if (staminaFloat < 0) staminaFloat = 0;
            staminaSize.x = staminaFloat;
            StaminaBar.transform.localScale = staminaSize;
        }
        else
        {
            staminaFloat = PlayerManager.Instance.stamina / PlayerManager.Instance.maxStamina;
            staminaSize.x = staminaFloat;
            StaminaBar.transform.localScale = staminaSize;
        }
    }
    private void Health() {
        if (healthSize.x > .001f)
        {
            healthFloat = PlayerManager.Instance.health / PlayerManager.Instance.maxHeath;
            if (healthFloat < 0) healthFloat = 0;
            healthSize.x = healthFloat;
            HealthBar.transform.localScale = healthSize;
        }
    }
    private void Souls() {
        SoulsText.text = PlayerManager.Instance.souls.ToString();
    }
    private void Money() {
        MoneyText.text = PlayerManager.Instance.money.ToString();
    }
    private void Ammo()
    {
        RoundsText.text = playerScript.Rounds.ToString();
        TotalAmmoText.text = PlayerManager.Instance.totalAmmo.ToString();
    }
}
