using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] StudioEventEmitter ambienceEmitter;
    [SerializeField] StudioEventEmitter musicEmitter;

    const string mainMenu = "Main Menu";
    const string gameOver = "GameOver";
    const string shark = "Shark";

    private void Awake()
    {
        // Singleton
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ambienceEmitter.Play();

        musicEmitter.SetParameter(mainMenu, 1f);
        musicEmitter.Play();
    }

    public void StartGame()
    {
        musicEmitter.SetParameter(mainMenu, 0f);
    }
}
