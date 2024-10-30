using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Health enemyHealth;

    private void Update()
    {
        if (enemyHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
