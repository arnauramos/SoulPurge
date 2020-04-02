using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public AudioClip clip;
    public enum Songs
    {
        Menu,
        SafeZone,
        HostileZone,
        GameOver,
    }
    public List<AudioClip> clips;
    // Start is called before the first frame update
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

    public void PlaySong(Songs song)
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        switch (song)
        {
            case Songs.Menu:
                clip = clips[(int)Songs.Menu];
                break;
            case Songs.SafeZone:
                clip = clips[(int)Songs.SafeZone];
                break;
            case Songs.HostileZone:
                clip = clips[(int)Songs.HostileZone];
                break;
            case Songs.GameOver:
                clip = clips[(int)Songs.GameOver];
                break;
            default:
                break;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
