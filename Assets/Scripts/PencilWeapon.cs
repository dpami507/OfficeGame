using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PencilWeapon : WeaponBaseScript
{
    
    public override void Attack(int attackNumber)
    {
        //StartCoroutine(Wait(attackNumber));
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
            playerPencil.GetComponent<PencilScript>().damage = damage * damageUpgrade;
            playerPencil.GetComponent<PencilScript>().enemiesToPass = enemiesToPass;
        }
    }

    public override string LevelDescription(int level) {
        switch (level) {
            case 1:
                return "Fires at the nearest enemy.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Cooldown reduced by 0.2 seconds.";
            case 4:
                return "Fires 1 more projectile.";
            case 5:
                return "Base Damage up by 5.";
            case 6:
                return "Fires 1 more projectile.";
            case 7:
                return "Passes through 1 more enemy.";
            case 8:
                return "Base Damage up by 5.";
            default:
                return "Error";
        }
    }

    public override void LevelSelfUp(int level) {
        switch (level)
        {
            case 2:
                numAttacks++;
                break;
            case 3:
                attackCooldown -= 0.2f;
                break;
            case 4:
                numAttacks++;
                break;
            case 5:
                damage += 5;
                break;
            case 6:
                numAttacks++;
                break;
            case 7:
                enemiesToPass++;
                break;
            case 8:
                damage += 5;
                break;
            default:
                gameObject.SetActive(true);
                break;
        }
    }

}
