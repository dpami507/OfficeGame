using UnityEngine;

public class PrinterScript : BulletScript
{
    float lifespan = 0.0f;

    public override void SetOwnStats(float[] myNumStats, bool isInfinite)
    {
        base.SetOwnStats(myNumStats, isInfinite);
    }

    void FixedUpdate()
    {
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife)
        {
            Destroy(gameObject);
        }
    }
}
