using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowTutorial : MonoBehaviour
{
    [SerializeField] GameObject leftStick;
    [SerializeField] GameObject rightStick;

    void Start()
    {
        TutorialAnimation();
    }

    private void TutorialAnimation()
    {
        const float rotationTime = 1.2f;

        Sequence mySequence = DOTween.Sequence();
        mySequence
            .AppendInterval(.4f)
            .Append(leftStick.transform.DOLocalMoveY(24f, 1f))

            .Append(leftStick.transform.DOBlendableMoveBy(new Vector3(0f, -48f, 0), rotationTime).SetEase(Ease.InOutSine))
            .Join(leftStick.transform.DOBlendableMoveBy(new Vector3(-24f, 0, 0), rotationTime * .5f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo))

            .AppendInterval(.2f)
            .Append(leftStick.transform.DOLocalMove(Vector3.zero, .6f));
    }
}
