using System;
using UnityEngine;

public class PencilScript : BulletScript
{
    public float speed;
    public Vector3 direction = Vector3.zero;
    float lifespan = 0.0f;
    public float maxLife = 10.0f;
    public Rigidbody2D rb;
    void FixedUpdate()
    {
        transform.position -= direction * speed * Time.deltaTime;
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife)
        {
            Destroy(gameObject);
        }
    }
}
