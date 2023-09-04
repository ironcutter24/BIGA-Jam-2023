using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Menu;

    public int BestScore { get; private set; } = 0;

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

    public void SetScore(float yDistance)
    {
        BestScore = (int)yDistance * 100;
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
        AudioManager.Instance.ResetMusic();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
