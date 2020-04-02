using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
    public void QuitGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        SceneManager.LoadScene(1);
    }
}
