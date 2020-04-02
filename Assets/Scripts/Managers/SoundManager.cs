using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioClip clip;
    public enum Sounds
    {
        Shooting,
        Reloading,
        EnemyShooting,
        PlayerDamage,
        EnemyDamage,
        EnemyDie,
        PlayerDie,
    }
    public List<AudioClip> clips;

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
    public void PlaySound(Sounds sound)
    {
        GameObject soundGO = new GameObject("Sound");
        AudioSource audioSource = soundGO.AddComponent<AudioSource>();
        switch (sound)
        {
            case Sounds.Shooting:
                clip = clips[(int)Sounds.Shooting];
                break;
            case Sounds.Reloading:
                clip = clips[(int)Sounds.Reloading];
                break;
            case Sounds.EnemyShooting:
                clip = clips[(int)Sounds.EnemyShooting];
                break;
            case Sounds.PlayerDamage:
                clip = clips[(int)Sounds.PlayerDamage];
                break;
            case Sounds.EnemyDamage:
                clip = clips[(int)Sounds.EnemyDamage];
                break;
            case Sounds.EnemyDie:
                clip = clips[(int)Sounds.EnemyDie];
                break;
            case Sounds.PlayerDie:
                clip = clips[(int)Sounds.PlayerDie];
                break;
            default:
                break;
        }
        audioSource.PlayOneShot(clip);
        Destroy(soundGO, clip.length);
    }
}
