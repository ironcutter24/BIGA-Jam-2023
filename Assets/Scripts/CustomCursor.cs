using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Sprite cursorUp, cursorDown;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        gameObject.SetActive(GameManager.Instance.State == GameState.Menu);

        image.sprite = Mouse.current.leftButton.isPressed ? cursorDown : cursorUp;
        transform.position = Mouse.current.position.value;
    }
}
