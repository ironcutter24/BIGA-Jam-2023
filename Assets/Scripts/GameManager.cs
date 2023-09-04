using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Menu;

    private void Awake()
    {
        // Singleton
        if (!Instance) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameState state)
    {
        State = state;
    }

    public void GameOver()
    {
        StartCoroutine(_GameOver());
    }

    private IEnumerator _GameOver()
    {
        AudioManager.Instance.GameOverMusic();

        yield return new WaitForSeconds(8f);

        SetState(GameState.Menu);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        AudioManager.Instance.ResetMusic();
    }
}
