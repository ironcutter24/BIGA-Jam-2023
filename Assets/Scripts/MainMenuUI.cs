using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    RectTransform container;

    [SerializeField]
    float animSpeed = 2f;

    [SerializeField]
    Button startButton;

    [SerializeField]
    TextMeshProUGUI bestScore;

    Sequence mySequence;

    void Start()
    {
        InitMenu();
        InitSequence();
        mySequence.Restart();
    }

    void InitMenu()
    {
        startButton.onClick.AddListener(StartGame);
        container.gameObject.SetActive(true);
        transform.localPosition = -500f * Vector2.up;
        container.sizeDelta = new Vector2(836f, 150f);

        bestScore.text = GameManager.Instance.BestScore.ToString();
    }

    void StartGame()
    {
        GameManager.Instance.SetState(GameState.Rafting);
        AudioManager.Instance.StartGameMusic();

        mySequence.Complete();
        mySequence.PlayBackwards();
    }

    void InitSequence()
    {
        mySequence = DOTween.Sequence();
        mySequence
            .Append(transform.DOLocalMoveY(0f, animSpeed)
                .SetEase(Ease.OutBack)
                )
            .Insert(animSpeed * .25f, container.DOSizeDelta(new Vector2(container.sizeDelta.x, 1000), animSpeed * .6f)
                .SetEase(Ease.InSine)
            )
            .OnComplete(() => Debug.Log("Completed"))
            .OnRewind(() => StartCoroutine(DisplayTutorial()))
            .SetAutoKill(false)
            .Pause();
    }

    [SerializeField]
    GameObject tutorial;
    IEnumerator DisplayTutorial()
    {
        tutorial.SetActive(true);
        yield return new WaitForSeconds(14f);
        tutorial.SetActive(false);
    }
}
