using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowCameraClamped : MonoBehaviour
{
    [SerializeField]
    Vector2 step = new Vector2(10f, 10f);

    [SerializeField]
    Transform target;

    [SerializeField]
    GameObject content;

    private void Start()
    {
        content.SetActive(true);
    }

    void FixedUpdate()
    {
        var delta = target.position - transform.position;
        delta.x = Snap(delta.x, step.x);
        delta.y = Snap(delta.y, step.y);
        delta.z = 0f;

        transform.position += delta;
    }

    float Snap(float val, float step)
    {
        return Mathf.FloorToInt(val / step) * step;
    }
}
