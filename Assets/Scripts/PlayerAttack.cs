using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    public float attackCooldown;
    float lastAttack;

    public GameObject pencil;

    public Transform player;
    public Vector2 attackSquareSize;
    public EnemySpawner enemies; 

    public RaycastHit2D[] hits;

    private void Update()
    {
        lastAttack += Time.deltaTime;
        if(lastAttack > attackCooldown) 
        {
            BasicAttack();
            lastAttack = 0;
        }
    }
    /*
    void MeleeAttack()
    {
        hits = Physics2D.BoxCastAll(player.position, attackSquareSize, 0f, Vector2.up, .1f);
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("enemy")) 
            {
                hit.transform.GetComponent<EnemyManager>().enemyHealth.TakeDamage(damageAmount);
            }
        }
    }
    */
    void BasicAttack() {
        // find nearest enemy
        float closestDist = 1000000.0f;
        float temp;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies.spawnedEnemies) {
            temp = Mathf.Abs(Vector3.Distance(player.position, enemy.transform.position));
            if (temp < closestDist) {
                closestDist = temp;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null) {
            // technically all of these variables could be combined in the instantiate call but I would like a refrence o0f this code lol
            Vector2 posDiff = player.transform.position - closestEnemy.transform.position;
            Vector2 direction = posDiff.normalized;
            float rotAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Quaternion bulletRot = Quaternion.Euler(0, 0, rotAngle);
            GameObject playerPencil = Instantiate(pencil, player.transform.position, bulletRot);
            playerPencil.GetComponent<BulletScript>().direction = direction;
        }
    }
}
