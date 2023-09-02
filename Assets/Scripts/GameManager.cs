using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Menu;

    private void Awake()
    {
        // Singleton
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        
    }
}
