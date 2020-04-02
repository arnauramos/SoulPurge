using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlaySong(MusicManager.Songs.Menu);
    }
    public void Play()
    {
        if (!PlayerManager.Instance.tutorialDone)
        {
            checkTutorial();
        }
        PlayerManager.Instance.reset();
        DataManager.Instance.reset();
        MusicManager.Instance.PlaySong(MusicManager.Songs.SafeZone);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    private void checkTutorial()
    {
        BinaryReader reader;
        if (File.Exists("tutorial.bin"))
        {
            reader = new BinaryReader(File.Open("tutorial.bin", FileMode.Open));
            int done = reader.ReadInt32();
            reader.Close();
            if (done == 1)
            {
                PlayerManager.Instance.tutorialDone = true;
            }
            else
            {
                PlayerManager.Instance.tutorialDone = false;
            }
        }
        else
        {
            PlayerManager.Instance.tutorialDone = false;
        }
    }
}
