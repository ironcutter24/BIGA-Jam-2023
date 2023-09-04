using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator rowAnim;

    [SerializeField, Range(0f, 10f)]
    float linearDragX = 0f;

    [SerializeField, Range(0f, 10f)]
    float linearDragY = 0f;

    [SerializeField]
    float horizontalForce = 60f;

    float torque = 10f;

    [SerializeField]
    SpriteRenderer boat, hat;

    [SerializeField]
    MeshRenderer row;

    Rigidbody2D body;
    Gamepad gamepad;

    bool isDead = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        gamepad = Gamepad.current;
    }

    private void Start()
    {
        StartCoroutine(_ProcessInput());
    }

    private void FixedUpdate()
    {
        var relVel = transform.InverseTransformVector(body.velocity);
        relVel.x = Mathf.Lerp(relVel.x, 0f, linearDragX * Time.deltaTime);
        relVel.y = Mathf.Lerp(relVel.y, 0f, linearDragY * Time.deltaTime);
        body.velocity = transform.TransformVector(relVel);

        if (!isDead)
            body.AddForce(3f * Vector2.up);

        body.velocity = Vector2.ClampMagnitude(body.velocity, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            StartCoroutine(_Death());
        }
    }

    IEnumerator _Death()
    {
        isDead = true;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;

        GameManager.Instance.GameOver();

        yield return new WaitForSeconds(.4f);
        DOTween.To(() => boat.color.a, a => boat.color = ApplyAlpha(boat.color, a), 0f, .2f);
        yield return new WaitForSeconds(.2f);
        row.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);
        DOTween.To(() => hat.color.a, a => hat.color = ApplyAlpha(hat.color, a), 0f, .2f);
    }

    Color ApplyAlpha(Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }

    #region Input Events

    public void OnRowLeft()
    {
        MoveLeft(horizontalForce, torque);
    }

    public void OnRowRight()
    {
        MoveRight(horizontalForce, torque);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Controller inputs

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
                    GetAngleLeft,
                    MoveLeft
                    );
            }
            else if (IsInStartPosition(GetMoveRight()))
            {
                Debug.Log("Started right row!");
                yield return _ProcessRow(
                    GetMoveRight,
                    GetAngleRight,
                    MoveRight
                    );
            }

            yield return null;
        }
    }

    IEnumerator _ProcessRow(Func<Vector2> getMove, Func<Vector2, float> getAngle, Action<float, float> setForce)
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
                setForce(horizontalForce, torque);

                yield return new WaitUntil(() => getMove().magnitude < .1f);
                yield break;
            }

            oldMove = move;
            yield return null;
        }
    }

    #endregion

    #region Helpers

    private void MoveLeft(float force, float torque)
    {
        Move(force, -Mathf.Abs(torque));
        rowAnim.SetTrigger("RowLeft");
    }

    private void MoveRight(float force, float torque)
    {
        Move(force, Mathf.Abs(torque));
        rowAnim.SetTrigger("RowRight");
    }

    private void Move(float force, float torque)
    {
        if (GameManager.Instance.State != GameState.Rafting)
            return;

        if (isDead) return;

        body.AddRelativeForce(force * Vector2.up, ForceMode2D.Force);
        body.AddTorque(torque, ForceMode2D.Force);
    }

    bool IsInStartPosition(Vector2 move)
    {
        return move.y > .9f && Mathf.Abs(move.x) < .2f;
    }

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
