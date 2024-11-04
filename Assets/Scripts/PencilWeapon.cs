using UnityEngine;

public class PencilWeapon : MonoBehaviour
{
    //public int damageAmount;
    public float attackCooldown;
    float lastAttack;

    public GameObject pencil;
    public WaveSpawner enemies;

    private void Update()
    {
        lastAttack += Time.deltaTime;
        if (lastAttack > attackCooldown)
        {
            BasicAttack();
            lastAttack = 0;
        }
    }

    void BasicAttack()
    {
        // find nearest enemy
        float closestDist = 1000000.0f;
        float temp;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies.spawnedEnemies)
        {
            temp = Mathf.Abs(Vector3.Distance(transform.position, enemy.transform.position));
            if (temp < closestDist)
            {
                closestDist = temp;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null)
        {
            // technically all of these variables could be combined in the instantiate call but I would like a refrence o0f this code lol
            Vector2 posDiff = transform.position - closestEnemy.transform.position;
            Vector2 direction = posDiff.normalized;
            float rotAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Quaternion bulletRot = Quaternion.Euler(0, 0, rotAngle);
            GameObject playerPencil = Instantiate(pencil, transform.position, bulletRot);
            playerPencil.GetComponent<BulletScript>().direction = direction;
        }
    }
}
