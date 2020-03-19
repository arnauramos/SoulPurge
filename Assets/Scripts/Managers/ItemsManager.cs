using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance { get; private set; }

    public List<Usable> UsablesList;
    public List<Gun> GunsList;
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
    public Usable GetRandomUsable()
    {
        int i = Random.Range(0, UsablesList.Count);
        return UsablesList[i];
    }
    public Gun GetRandomGun()
    {
        int i = Random.Range(0, GunsList.Count);
        return GunsList[i];
    }
}
