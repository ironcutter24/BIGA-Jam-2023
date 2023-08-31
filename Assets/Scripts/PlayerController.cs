using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Gamepad gamepad;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        gamepad = Gamepad.current;
    }

    private void Start()
    {
        StartCoroutine(_ProcessInput());
    }

    IEnumerator _ProcessInput()
    {
        var move = Vector2.zero;

        while (true)
        {
            if (gamepad == null)
            {
                gamepad = Gamepad.current;
                yield return null;
                yield break;
            }

            if (IsInStartPosition(GetMoveLeft()))
            {
                Debug.Log("Started left row!");
                yield return _ProcessRow(
                    GetMoveLeft,
                    GetAngleLeft
                    );
            }
            else if (IsInStartPosition(GetMoveRight()))
            {
                Debug.Log("Started right row!");
                yield return _ProcessRow(
                    GetMoveRight,
                    GetAngleRight
                    );
            }

            yield return null;
        }
    }

    IEnumerator _ProcessRow(Func<Vector2> getMove, Func<Vector2, float> getAngle)
    {
        Vector2 move, oldMove;
        oldMove = getMove();

        while (true)
        {
            move = getMove();

            if (move.magnitude < .9f || getAngle(move) + 5f < getAngle(oldMove))
            {
                Debug.Log("Move failed.");
                yield break;
            }

            if (getAngle(move) >= 270f)
            {
                Debug.Log("Move completed!");
                yield break;
            }

            oldMove = move;
            yield return null;
        }
    }

    bool IsInStartPosition(Vector2 move)
    {
        return move.y > .9f && Mathf.Abs(move.x) < .2f;
    }

    #region Inputs

    Vector2 GetMoveLeft() { return gamepad.leftStick.ReadValue(); }

    Vector2 GetMoveRight() { return gamepad.rightStick.ReadValue(); }

    float GetAngleLeft(Vector2 move) { return GetAngle(Vector2.right, move); }

    float GetAngleRight(Vector2 move) { return 360f - GetAngle(Vector2.left, move); }

    float GetAngle(Vector2 from, Vector2 to)
    {
        var angle = Vector2.SignedAngle(from, to);
        if (angle < 0f)
            return 360f - Mathf.Abs(angle);
        else
            return angle;
    }

    #endregion

}
