using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioMixerGroup mixer;
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
        Steps,
        WoodenSteps,
        KeyPickup,
        SoulPickup1, 
        SoulPickup2, 
        SoulPickup3, 
        Shop,
        SwapWeapon,
        Door,
        ItemPickup,
        UseUpgrade,
        UsePotion,
        UseKit,
        UseAmmo
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
        audioSource.outputAudioMixerGroup = mixer;
        clip = clips[(int)sound];
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
            Destroy(soundGO, clip.length);
        }
    }
}
