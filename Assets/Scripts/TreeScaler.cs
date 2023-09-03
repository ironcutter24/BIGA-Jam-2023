using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScaler : MonoBehaviour
{
    [SerializeField]
    Vector2 scaleFactor = Vector2.zero;

    private void Start()
    {
        float delta = Random.Range(scaleFactor.x, scaleFactor.y);
        transform.localScale += (Vector3)(delta * Vector2.one);
    }
}
