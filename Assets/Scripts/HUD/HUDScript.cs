using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    // HOSTILE HUD
    public GameObject HostileHud;
    // SCENE
    private ThisScene thisScene;
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
    public GameObject MoneyUI;
    public TextMeshProUGUI MoneyText;

    // KEYS
    public GameObject KeysUI;
    public TextMeshProUGUI KeysText;

    //// AMMO
    //public GameObject RoundsCounter;
    //private TextMeshProUGUI RoundsText;
    //public GameObject TotalAmmoCounter;
    //private TextMeshProUGUI TotalAmmoText;

    // PLAYER
    private GameObject player;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        // SCENE
        thisScene = GameObject.Find("ThisScene").GetComponent<ThisScene>();

        // HEALTH
        HealthImage = HealthBar.GetComponent<Image>();

        // STAMINA
        StaminaImage = StaminaBar.GetComponent<Image>();

        // SOULS
        SoulsText = SoulsCounter.GetComponent<TextMeshProUGUI>();

        //// AMMO
        //RoundsText = RoundsCounter.GetComponent<TextMeshProUGUI>();
        //TotalAmmoText = TotalAmmoCounter.GetComponent<TextMeshProUGUI>();

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
        Keys();
        //Ammo();
        HostileHUD();
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
        if (thisScene.scene == ThisScene.Scene.SAFEZONE)
        {
            MoneyUI.SetActive(true);
            MoneyText.text = PlayerManager.Instance.money.ToString();
        }
        else
        {
            MoneyUI.SetActive(false);
        }
    }
    private void Keys()
    {
        if (thisScene.scene == ThisScene.Scene.HOSTILEZONE)
        {
            KeysUI.SetActive(true);
            KeysText.text = PlayerManager.Instance.keys.ToString();
        }
        else
        {
            KeysUI.SetActive(false);
        }
    }
    //private void Ammo()
    //{
    //    if (!PlayerManager.Instance.reloading)
    //    {
    //        RoundsText.text = playerScript.Rounds.ToString();
    //        TotalAmmoText.text = PlayerManager.Instance.totalAmmo.ToString();
    //    }
    //}
    private void HostileHUD()
    {
        if (thisScene.scene == ThisScene.Scene.HOSTILEZONE)
        {
            HostileHud.SetActive(true);
        }
        else
        {
            HostileHud.SetActive(false);
        }
    }
}
