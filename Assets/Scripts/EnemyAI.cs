using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject xpGemToSpawn;
    Health myHealth;
    Transform target;
    SpriteRenderer sprite;

    public Sprite[] backgroundSprites;

    bool moving;
    public float speed;

    Rigidbody2D rb;
    float tDist;

    bool attacking;
    public float attackDist;
    public float lastCooldownTime;
    public float lastAttackTime;
    public int damage;

    public GameObject blood;


    private void Start()
    {
        target = FindFirstObjectByType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

    }

    private void Update()
    {
        tDist = Vector2.Distance(transform.position, target.position);

        if (tDist <= attackDist/3)
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

        if(target.position.x < transform.position.x)
        {
            sprite.flipX = true;   
        }
        else
        {
            sprite.flipX = false;
        }
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
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

        FindAnyObjectByType<WaveSpawner>().spawnedEnemies.Remove(this.gameObject);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack") {
            myHealth.TakeDamage(collision.GetComponent<BulletScript>().damage);
            if (collision.GetComponent<BulletScript>().enemiesToPass <= 0 && !collision.GetComponent<BulletScript>().infinitePass)
            {
                Destroy(collision.gameObject);
            }
            else if (collision.GetComponent<BulletScript>().infinitePass) {
                return;
            }
            else {
                collision.GetComponent<BulletScript>().enemiesToPass--;
            }
        }
    }
}
