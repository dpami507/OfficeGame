using UnityEngine;

public class PencilWeapon : WeaponBaseScript
{
    public override void Attack()
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
            GameObject playerPencil = Instantiate(bullet, transform.position, bulletRot);
            playerPencil.GetComponent<PencilScript>().direction = direction;
        }
    }
}
