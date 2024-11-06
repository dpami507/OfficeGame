using UnityEngine;

public class WeaponBaseScript : MonoBehaviour
{
    public float attackCooldown;
    public float lastAttack;
    public WaveSpawner enemies;
    public GameObject bullet;
    public int level = 0;
    public string nameWeapon;

    private void Start()
    {
        enemies = FindFirstObjectByType<WaveSpawner>();
    }

    // is virtual to allow it to use the subclass's attack function without having to add a seperate update function there
    public virtual void Update()
    {
        lastAttack += Time.deltaTime;
        if (lastAttack > attackCooldown)
        {
            Attack();
            lastAttack = 0;
        }
    }
    public virtual void Attack() {
        Debug.Log("Attack function was not overrriden");
    }
}
