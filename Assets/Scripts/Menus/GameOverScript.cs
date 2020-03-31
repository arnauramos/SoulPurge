using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    private TextMeshProUGUI BulletsShot;
    private int bullets;
    private TextMeshProUGUI MoneySpent;
    private int money;
    private TextMeshProUGUI ObtainedSouls; 
    private int souls;
    private TextMeshProUGUI DistanceTravelled; 
    private int distance;
    private void Start()
    {
        BulletsShot = GameObject.Find("BulletsShotCounter").GetComponent<TextMeshProUGUI>();
        MoneySpent = GameObject.Find("MoneySpentCounter").GetComponent<TextMeshProUGUI>();
        ObtainedSouls = GameObject.Find("ObtainedSoulsCounter").GetComponent<TextMeshProUGUI>();
        DistanceTravelled = GameObject.Find("DistanceTravelledCounter").GetComponent<TextMeshProUGUI>();
        bullets = 0;
        money = 0;
        souls = 0;
        distance = 0;
    }
    private void Update()
    {
        BulletsCount();
        MoneyCount();
        SoulsCount();
        DistanceCount();
    }
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void BulletsCount()
    {
        if (bullets < DataManager.Instance.BulletsShot)
        {
            bullets++;
        }
        BulletsShot.text = bullets.ToString();
    }
    private void MoneyCount() 
    {
        if (money < DataManager.Instance.MoneySpent)
        {
            money++;
        }
        MoneySpent.text = money.ToString();
    }
    private void SoulsCount() 
    { 
        if (souls < DataManager.Instance.ObtainedSouls)
        {
            souls++;
        }
        ObtainedSouls.text = souls.ToString();
    }
    private void DistanceCount() 
    { 
        if (distance < DataManager.Instance.DistanceTravelled)
        {
            distance++;
        }
        DistanceTravelled.text = distance.ToString();
    }
}
