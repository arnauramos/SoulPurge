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
    private GameObject WeaponsShopper;
    private GameObject SoulsShopper;
    private GameObject shop;
    public bool UsablesShopping;
    public bool WeaponsShopping;
    public bool SoulsShopping;
    public bool OpenUsablesShop;
    public bool OpenWeaponsShop;
    public bool OpenSoulsShop;

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
    public void SoulsExchange(int _souls, int _price)
    {
        PlayerManager.Instance.souls -= _souls;
        PlayerManager.Instance.addMoney(_souls * _price);
    }
    // SOULS SHOP
    public void SoulsShop(GameObject Shopper)
    {
        // DESHABILITAR MOVIMIENTO, APUNTADO, DISPARAR Y OBJETOS DEL JUGADOR
        PlayerManager.Instance.playerDisabled = true;
        // GET CAMERA MANAGER
        Instance.CameraController = GameObject.Find("CameraController");
        // ENCONTRAR GAMEOBJECTS TIENDA
        SoulsShopper = GameObject.Find("SoulsExchange");
        shop = SoulsShopper.transform.GetChild(1).gameObject;
        // GET HUD
        HUD = CameraController.transform.GetChild(0).GetChild(0).gameObject;
        // DESHABILITAR HUD
        HUD.SetActive(false);
        // MOSTRAR TIENDA
        shop.SetActive(true);
        shop.transform.position = CameraController.transform.position;
        // HABILITAR USABLES SHOPPING
        SoulsShopping = true;
        OpenSoulsShop = true;
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
        OpenUsablesShop = true; 
    }
    public void WeaponsShop(GameObject Shopper)
    {
        // DESHABILITAR MOVIMIENTO, APUNTADO, DISPARAR Y OBJETOS DEL JUGADOR
        PlayerManager.Instance.playerDisabled = true;
        // GET CAMERA MANAGER
        Instance.CameraController = GameObject.Find("CameraController");
        // ENCONTRAR GAMEOBJECTS TIENDA
        WeaponsShopper = GameObject.Find("WeaponsShop");
        shop = WeaponsShopper.transform.GetChild(1).gameObject;
        // GET HUD
        HUD = CameraController.transform.GetChild(0).GetChild(0).gameObject;
        // DESHABILITAR HUD
        HUD.SetActive(false);
        // MOSTRAR TIENDA
        shop.SetActive(true);
        shop.transform.position = CameraController.transform.position;
        // HABILITAR WEAPON SHOPPING
        WeaponsShopping = true;
        OpenWeaponsShop = true;
    }

    public void CloseSoulsShop()
    {
        CloseShop();

        //DESHABILITAR SOULS SHOP
        SoulsShopper = GameObject.Find("SoulsExchange");
        shop = SoulsShopper.transform.GetChild(1).gameObject;
        shop.SetActive(false);
        Debug.Log("5");
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
    public void CloseWeaponsShop()
    {
        CloseShop();

        //DESHABILITAR WEAPONS SHOP
        WeaponsShopper = GameObject.Find("WeaponsShop");
        shop = WeaponsShopper.transform.GetChild(1).gameObject;
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
