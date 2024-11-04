using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    Vector3 vel = Vector3.zero;
    public float smoothTime;
    
    public Vector3 offset;
    public Rigidbody2D rb;
    public float rbSmooth;
    
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset + (new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0f) / rbSmooth);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothTime);
    }
}
