using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    // CAMERA
    private GameObject CameraController;
    private GameObject HUD;
    // SOUL EXCHANGE
    public int moneyToGive;

    // SHOPS
    private GameObject UsablesShopper;
    private GameObject shop;
    public bool UsablesShopping;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Error: Duplicated " + this + "in the scene");
        }
    }

    //  SOULER / SOUL EXCHANGER
    public void SoulsExchange(int _souls)
    {
        PlayerManager.Instance.souls -= _souls;
        PlayerManager.Instance.addMoney(_souls * 10);
    }

    //  USABLES SHOP
    public void UsablesShop(GameObject Shopper)
    {
        // DESHABILITAR MOVIMIENTO, APUNTADO, DISPARAR Y OBJETOS DEL JUGADOR
        PlayerManager.Instance.playerDisabled = true;
        // GET CAMERA MANAGER
        Instance.CameraController = GameObject.Find("CameraController");
        // ENCONTRAR GAMEOBJECTS TIENDA
        UsablesShopper = GameObject.Find("UsablesShop");
        shop = UsablesShopper.transform.GetChild(1).gameObject;
        // GET HUD
        HUD = CameraController.transform.GetChild(0).GetChild(0).gameObject;
        // DESHABILITAR HUD
        HUD.SetActive(false);
        // MOSTRAR TIENDA
        shop.SetActive(true);
        shop.transform.position = CameraController.transform.position;
        // HABILITAR USABLES SHOPPING
        UsablesShopping = true; 
    }

    public void CloseUsablesShop()
    {
        CloseShop();

        //DESHABILITAR USABLES SHOP
        UsablesShopper = GameObject.Find("UsablesShop");
        shop = UsablesShopper.transform.GetChild(1).gameObject;
        shop.SetActive(false);
        Debug.Log("5");
    }

    private void CloseShop()
    {
        Debug.Log("1");

        // DESHABILITAR USABLES SHOPPING
        UsablesShopping = false;
        Debug.Log("2");

        // HABILITAR MOVIMIENTO, APUNTADO, DISPARAR Y OBJETOS DEL JUGADOR 
        PlayerManager.Instance.playerDisabled = false;
        Debug.Log("3");

        // MOSTRAR HUD
        CameraController = GameObject.Find("CameraController");
        HUD = CameraController.transform.GetChild(0).GetChild(0).gameObject;
        HUD.SetActive(true);
        Debug.Log("4");
    }

    private GameObject FindChild(GameObject Parent, string Name)
    {
        foreach (Transform t in Parent.transform)
        {
            if (t.name == Name)
            {
                return t.gameObject;
            }
        }
        return null;
    }
}
