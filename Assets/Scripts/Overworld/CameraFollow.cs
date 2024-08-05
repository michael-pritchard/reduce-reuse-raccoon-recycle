using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Maintain the initial offset
        transform.position = player.position + offset;
    }
}
