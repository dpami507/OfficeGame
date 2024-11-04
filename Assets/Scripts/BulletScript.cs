using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public Vector3 direction = Vector3.zero;
    float lifespan = 0.0f;
    public float maxLife = 10.0f;
    public Rigidbody2D rb;
    public float damage = 5;

    void FixedUpdate()
    {
        transform.position -= direction * speed * Time.deltaTime;
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife) {
            Destroy(gameObject);
        }
    }
}
