using UnityEngine;

public class FindClosestObject : MonoBehaviour
{
    public GameObject closestObject;
    public WeaponBaseScript weapon;

    private void Update()
    {
        // find nearest enemy
        float closestDist = 1000000.0f;
        float temp;

        foreach (GameObject enemy in weapon.enemies.spawnedEnemies)
        {
            if(enemy != null)
            {
                temp = Mathf.Abs(Vector3.Distance(transform.position, enemy.transform.position));
                if (temp < closestDist)
                {
                    closestDist = temp;
                    closestObject = enemy;
                }
            }
        }
    }
}
