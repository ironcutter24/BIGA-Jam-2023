using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    RectTransform container;

    IEnumerator Start()
    {
        transform.DOLocalMoveY(0f, 2f)
            .SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.6f);

        container.DOSizeDelta(new Vector2(container.sizeDelta.x, 1000), 1.4f)
            .SetEase(Ease.InSine);
    }
}
