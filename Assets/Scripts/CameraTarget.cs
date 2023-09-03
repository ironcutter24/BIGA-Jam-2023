using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    public void FixedUpdate()
    {
        var pos = target.position;
        transform.position = new Vector3(0f, pos.y, 0f);
    }
}
