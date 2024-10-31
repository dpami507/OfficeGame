using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    public float attackCooldown;
    float lastAttack;

    public Transform attackSquare;
    public Vector2 attackSquareSize;

    public RaycastHit2D[] hits;

    private void Update()
    {
        if(Input.GetMouseButton(0) && lastAttack + attackCooldown < Time.time) 
        {
            MeleeAttack();
            lastAttack = Time.time;
        }
    }

    void MeleeAttack()
    {
        hits = Physics2D.BoxCastAll(attackSquare.position, attackSquareSize, 0f, Vector2.up, .1f);
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("enemy")) 
            {
                hit.transform.GetComponent<EnemyManager>().enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
}
