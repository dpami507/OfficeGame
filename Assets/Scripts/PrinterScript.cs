using UnityEngine;

public class PrinterScript : BulletScript
{
    float lifespan = 0.0f;
    public float maxLife = 10.0f;
    void FixedUpdate()
    {
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife)
        {
            Destroy(gameObject);
        }
    }
}
