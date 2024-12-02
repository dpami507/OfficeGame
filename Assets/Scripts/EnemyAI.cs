using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject[] xpGemToSpawn;
    public Vector2 xpRolls;

    Health myHealth;
    Transform target;
    public SpriteRenderer sprite;

    public Color[] hairColors;

    public GameObject[] hairChoices;
    public GameObject selectedHair;

    bool moving;
    public float speed;
    public float antiSlide;

    Rigidbody2D rb;
    float tDist;

    bool attacking;
    public float attackDist;
    public float lastCooldownTime;
    public float lastAttackTime;
    public int damage;
    public bool gotHit = false;
    //public float knockstunTime = 0.1f;
    public float knockbackTotal = 0.0f;

    public GameObject blood;

    Vector2 storedVel;
    bool gameRunning;
    GameManager manager;


    private void Start()
    {
        target = FindFirstObjectByType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        manager = FindFirstObjectByType<GameManager>();
        gameRunning = manager.gameRunning;

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
        if (gameRunning != manager.gameRunning)
        {
            if (manager.gameRunning)
            {
                gameRunning = true;
                rb.linearVelocity = storedVel;
            }
            else
            {
                gameRunning = false;
                storedVel = rb.linearVelocity;
                rb.linearVelocity = Vector3.zero;
            }
        }

        if (gameRunning == false) { return; }

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
        if (!moving && !gotHit) { rb.linearVelocity = Vector2.zero; return; }

        float xDist = (target.position.x - transform.position.x) / tDist;
        float yDist = (target.position.y - transform.position.y) / tDist;

        rb.linearVelocity = new Vector2(xDist * speed, yDist * speed);
        // do knockback here
        if (gotHit) {
            rb.linearVelocity *= knockbackTotal;
            rb.linearVelocity = new Vector2(xDist * 4 * knockbackTotal, yDist * 4 * knockbackTotal);
            knockbackTotal += 0.1f;
            if (knockbackTotal >= 0) {
                gotHit = false;
                knockbackTotal = 0;
            }
        }

        Vector3 currentVel = rb.linearVelocity;
        Vector3 velChange = (desiredVel - currentVel) * antiSlide;

        rb.AddForce(velChange, ForceMode2D.Force);

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

        int x = Mathf.RoundToInt(Random.Range(xpRolls.x, xpRolls.y));
        for (int i = 0; i < x; i++)
        {
            Instantiate(xpGemToSpawn[Random.Range(0, xpGemToSpawn.Length)], transform.position, randRot);
        }
        Destroy(Instantiate(blood, transform.position, Quaternion.identity), 1f);

        FindAnyObjectByType<WaveSpawner>().spawnedEnemies.Remove(this.gameObject);

        FindFirstObjectByType<DeathScreenManager>().kills++;

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack") {
            myHealth.TakeDamage(collision.GetComponent<BulletScript>().damage);
            // add knockback here
            if (knockbackTotal > -10f * speed)
            {
                knockbackTotal += collision.GetComponent<BulletScript>().knockback;
            }
            else {
                knockbackTotal = -10f * speed;
            }

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
            gotHit = true;
        }
    }
}
