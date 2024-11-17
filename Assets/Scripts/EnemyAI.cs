using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject[] xpGemToSpawn;
    Health myHealth;
    Transform target;
    public SpriteRenderer sprite;

    public Color[] hairColors;

    public GameObject[] hairChoices;
    public GameObject selectedHair;

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

        foreach (GameObject choice in hairChoices)
        {
            choice.SetActive(false);
        }

        selectedHair = hairChoices[Random.Range(0, hairChoices.Length)].gameObject;
        selectedHair.SetActive(true);
        selectedHair.GetComponent<SpriteRenderer>().color = hairColors[Random.Range(0, hairColors.Length)];

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
            selectedHair.GetComponent<SpriteRenderer>().flipX = true;   
        }
        else
        {
            sprite.flipX = false;
            selectedHair.GetComponent<SpriteRenderer>().flipX = false;
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
        Quaternion randRot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(xpGemToSpawn[Random.Range(0, xpGemToSpawn.Length)], transform.position, randRot);
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

        FindAnyObjectByType<WaveSpawner>().spawnedEnemies.Remove(this.gameObject);

        FindFirstObjectByType<DeathScreenManager>().kills++;

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
