using UnityEngine;

public class PrinterWeapon : MonoBehaviour
{
    //public int damageAmount;
    public float attackCooldown;
    float lastAttack;

    public GameObject printer;

    private void Update()
    {
        lastAttack += Time.deltaTime;
        if (lastAttack > attackCooldown)
        {
            Attack();
            lastAttack = 0;
        }
    }

    public void Attack()
    {
        Instantiate(printer, transform.position, Quaternion.identity);
    }
}
