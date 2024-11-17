using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    Vector3 vel = Vector3.zero;
    public float smoothTime;
    
    public Vector3 offset;
    public Rigidbody2D rb;
    public float rbSmooth;

    public float rotStrength;
    public float shakeStrength;

    Vector3 desiredPosition;
    
    
    private void FixedUpdate()
    {
        desiredPosition = target.position + offset + (new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0f) / rbSmooth);

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, smoothTime);
    }

    public void Shake()
    {
        int shakeAmount = RandomNumber();

        transform.position = new Vector3(transform.position.x + shakeAmount * shakeStrength, transform.position.y + shakeAmount * shakeStrength, -10);
        transform.rotation = Quaternion.Euler(0, 0, rotStrength * shakeAmount);
    }

    public void ForcePos()
    {
        transform.position = desiredPosition;
        transform.rotation = Quaternion.identity;
    }

    int RandomNumber()
    {
        int temp = UnityEngine.Random.Range(-1, 1);

        if (temp == 0)
            return -1;
        else
            return 1;
    }
}
