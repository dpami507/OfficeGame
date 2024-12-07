using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce = 15f; // Force of the explosion
    public float explosionRadius = 5f; // Radius of the explosion

    void Explode()
    {
        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (!collider.CompareTag("enemy")) continue;

            // Check if the object has a Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Calculate the direction from the explosion center to the object
                Vector2 explosionDirection = rb.position - (Vector2)transform.position;
                float distance = explosionDirection.magnitude;

                // Normalize the direction and calculate the force based on distance
                explosionDirection.Normalize();
                float force = Mathf.Lerp(explosionForce, 0, distance / explosionRadius);

                // Apply the force
                rb.AddForce(explosionDirection * force, ForceMode2D.Impulse);
            }
        }
    }

    // Test the explosion by pressing a key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }
}
