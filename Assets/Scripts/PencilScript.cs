using System;
using UnityEngine;

public class PencilScript : BulletScript
{
    public Vector3 direction = Vector3.zero;
    float lifespan = 0.0f;
    public Rigidbody2D rb;

    public override void SetOwnStats(float[] myNumStats, bool isInfinite)
    {
        base.SetOwnStats(myNumStats, isInfinite);
    }

    void FixedUpdate()
    {
        if(!gameRunning) { return; }
        transform.position -= direction * speed * Time.deltaTime;
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife)
        {
            Destroy(gameObject);
        }
    }
}
