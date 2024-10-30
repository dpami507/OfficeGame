using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    public float attackCooldown;
    float lastAttack;

    public Transform attackSquare;
    public Vector2 attackSquareSize;

    void Attack()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(attackSquare.position, attackSquareSize, 0f, Vector2.one);
        
        foreach (RaycastHit2D hit in hits)
        {
            hit.transform.GetComponent<EnemyManager>().enemyHealth.TakeDamage(damageAmount);
        }
    }
}
