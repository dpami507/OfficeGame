using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Health enemyHealth;
    public GameObject xpGemToSpawn;

    private void Update()
    {
        if (enemyHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(xpGemToSpawn, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
