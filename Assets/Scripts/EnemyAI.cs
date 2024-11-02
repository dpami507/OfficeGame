using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject xpGemToSpawn;
    Health myHealth;
    Transform target;

    bool moving;
    public float speed;

    Rigidbody2D rb;
    float tDist;

    bool attacking;
    public float attackDist;
    public float lastCooldownTime;
    public float lastAttackTime;
    public int damage;


    private void Start()
    {
        target = FindFirstObjectByType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
    }

    private void Update()
    {
        tDist = Vector2.Distance(transform.position, target.position);

        if (tDist <= attackDist)
        {
            Attack();
            attacking = true;
            moving = false;
        }
        else
        {
            attacking = false;
            moving = true;
        }

        if (myHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        // movement
        if (!moving) { rb.linearVelocity = Vector2.zero; return; }

        float xDist = (target.position.x - transform.position.x) / tDist;
        float yDist = (target.position.y - transform.position.y) / tDist;

        rb.linearVelocity = new Vector2(xDist * speed, yDist * speed);
    }

    void Attack()
    {
        if(lastAttackTime + lastCooldownTime < Time.time && attacking)
        {
            Debug.Log("kapow");
            target.GetComponent<Health>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    void Die()
    {
        Instantiate(xpGemToSpawn, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack") {
            myHealth.TakeDamage(collision.GetComponent<BulletScript>().damage);
            Destroy(collision.gameObject);
        }
    }
}
