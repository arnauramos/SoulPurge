﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour
{
    public static PlayerSceneManager Instance { get; private set; }

    public int ActualIndexScene;
    public bool ZoneIsHostile;
    public int WhichZH;

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

    public int getActualScene()
    {
        ActualIndexScene = SceneManager.GetActiveScene().buildIndex;

        isSceneHostile();

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

    public void isSceneHostile()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName == "DEV_TileMap-ZonaHostil" || SceneName == "DEV_TileMap-ZonaHostil-2" || SceneName == "DEV_TileMap-ZonaHostil-3") ZoneIsHostile = true;
        else ZoneIsHostile = false;

        if (ZoneIsHostile)
        {
            int SceneLength = SceneName.Length - 1;
            switch (SceneName[SceneLength])
            {
                case '1':
                    WhichZH = 0;
                    break;
                case '2':
                    WhichZH = 1;
                    break;
                case '3':
                    WhichZH = 2;
                    break;
                default:
                    WhichZH = 0;
                    break;
            }
        }
    }
}
