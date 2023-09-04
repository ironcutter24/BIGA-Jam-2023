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
        ResetMusic();
    }

    public void StartGameMusic()
    {
        musicEmitter.SetParameter(mainMenu, 0f);
    }

    public void ResetMusic()
    {
        musicEmitter.Stop();
        musicEmitter.SetParameter(mainMenu, 1f);
        musicEmitter.SetParameter(gameOver, 0f);
        musicEmitter.Play();
    }

    public void GameOverMusic()
    {
        musicEmitter.SetParameter(gameOver, 1f);
    }
}
