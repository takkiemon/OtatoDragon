using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 2.0f * Time.deltaTime);
    }
}
