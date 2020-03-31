using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour
{
    public static PlayerSceneManager Instance { get; private set; }

    public int ActualIndexScene;
    public bool ZoneIsSecure;

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
        ZoneIsSecure = true;
    }

    public int getActualScene()
    {
        ActualIndexScene = SceneManager.GetActiveScene().buildIndex;

        isSceneSecure();

        return ActualIndexScene;
    }

    public void goBackScene(int value)
    {
        if (value - 1 <= 1) return; //  CHECKS IF THE SCENE IS LESS THAN THE MINOR SECURE ZONE (secure zones start at 2)
        SceneManager.LoadScene(value - 1);
    }

    public void goFrontScene(int value)
    {
        if (value + 1 >= SceneManager.sceneCountInBuildSettings) return; // CHECKS IF THERE'S AN ACTUAL SCENE
        SceneManager.LoadScene(value + 1);
    }
    public void goLastScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void isSceneSecure()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName == "DEV_TileMap-ZonaSegura" || SceneName == "ZonaSegura_Inicial") ZoneIsSecure = true;
        else ZoneIsSecure = false;
    }
}
