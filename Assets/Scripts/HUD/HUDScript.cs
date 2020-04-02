using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    // STAMINA
    public GameObject StaminaBar;
    private Image StaminaImage;
    private float staminaFloat = 1f;
    // STAMINA TEXT
    public TextMeshProUGUI StaminaText;

    // HEALTH
    public GameObject HealthBar;
    private Image HealthImage;
    private float healthFloat = 1f;
    // HEALTH TEXT
    public TextMeshProUGUI HealthText;

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
        // HEALTH
        HealthImage = HealthBar.GetComponent<Image>();

        // STAMINA
        StaminaImage = StaminaBar.GetComponent<Image>();

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
        staminaFloat = PlayerManager.Instance.stamina / PlayerManager.Instance.maxStamina;
        if (staminaFloat < 0) staminaFloat = 0;
        StaminaImage.fillAmount = staminaFloat;
        StaminaText.text = ((int)PlayerManager.Instance.stamina).ToString() + " / " + PlayerManager.Instance.maxStamina.ToString();
    }
    private void Health() {
        healthFloat = PlayerManager.Instance.health / PlayerManager.Instance.maxHeath;
        if (healthFloat < 0) healthFloat = 0;
        HealthImage.fillAmount = healthFloat;
        HealthText.text = ((int)PlayerManager.Instance.health).ToString() + " / " + PlayerManager.Instance.maxHeath.ToString();
    }
    private void Souls() {
        SoulsText.text = PlayerManager.Instance.souls.ToString();
    }
    private void Money() {
        MoneyText.text = PlayerManager.Instance.money.ToString();
    }
    private void Ammo()
    {
        if (!PlayerManager.Instance.reloading)
        {
            RoundsText.text = playerScript.Rounds.ToString();
            TotalAmmoText.text = PlayerManager.Instance.totalAmmo.ToString();
        }
    }
}
