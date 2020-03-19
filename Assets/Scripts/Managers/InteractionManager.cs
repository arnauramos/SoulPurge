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
    public GameObject shop;
    public GameObject exitButton;
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

    private void Update()
    {
        if (UsablesShopping)
        {
            // CAPTURAR ACCIONES JUGADOR

            // Cliquear usables, seleccionar cantidad, comprar, confirmar...

            // Botón de salir

            // INTERCAMBIO DE DINERO / USABLES

            // COMPRAR MÁS?

            // HABILITAR MOVIMIENTO, APUNTADO, DISPARAR Y OBJETOS DEL JUGADOR 
                //shop.SetActive(false);
                //PlayerManager.Instance.playerDisabled = false;
                //UsablesShopping = false;
                //return;
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
        CameraController = GameObject.Find("CameraController");
        // ENCONTRAR GAMEOBJECTS TIENDA
        shop = FindChild(Shopper, "UsablesShopMenu");
        // GET EXIT BUTTON
        exitButton = FindChild(shop, "UsablesShopExit");
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
