using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    float rotateSpeed = 3f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
