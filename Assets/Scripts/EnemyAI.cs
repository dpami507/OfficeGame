using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject[] xpGemToSpawn;
    public Vector2 xpRolls;

    [HideInInspector]
    public Health myHealth;
    public Transform target;
    public SpriteRenderer sprite;

    public Color[] hairColors;

    public GameObject[] hairChoices;
    public GameObject selectedHair;

    public bool moving;
    public float speed;
    public float antiSlide;

    Rigidbody2D rb;
    public float tDist;

    bool attacking;
    public float attackDist;
    public float lastCooldownTime;
    public float lastAttackTime;
    public int damage;
    public bool gotHit = false;
    //public float knockstunTime = 0.1f;
    public float knockbackTotal = 0.0f;

    public GameObject blood;

    public float shakePower = 1;
    public float rotPower = 1;

    Vector2 storedVel;

    public string enemyType;

    [HideInInspector]
    public GameManager manager;


    private void Start()
    {
        if (target == null)
            target = FindFirstObjectByType<PlayerManager>().transform;
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (myHealth == null)
            myHealth = GetComponent<Health>();
        if (manager == null)
            manager = FindFirstObjectByType<GameManager>();

        if (hairChoices.Length > 0)
        {
            this.gameObject.name = "enemy_" + Mathf.Round(Time.time) + "_" + enemyType;

            foreach (GameObject choice in hairChoices)
            {
                choice.SetActive(false);
            }

            selectedHair = hairChoices[Random.Range(0, hairChoices.Length)].gameObject;
            selectedHair.SetActive(true);
            selectedHair.GetComponent<SpriteRenderer>().color = hairColors[Random.Range(0, hairColors.Length)];
        }

        EnemyStart();
    }

    public void EnemyStart()
    {
        myHealth.currentHealth = myHealth.maxHealth;
        myHealth.UpdateHealthBar();
        gameObject.SetActive(true);
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

    public virtual void FixedUpdate()
    {
        // movement
        if (!moving && !gotHit) { rb.linearVelocity = Vector2.zero; return; }

        float xDist = (target.position.x - transform.position.x) / tDist;
        float yDist = (target.position.y - transform.position.y) / tDist;

        rb.linearVelocity = new Vector2(xDist * speed, yDist * speed);
        // do knockback here
        if (gotHit) {
            rb.linearVelocity = new Vector2(xDist * 4 * knockbackTotal, yDist * 4 * knockbackTotal);
            knockbackTotal += 0.1f;
            knockbackTotal = Mathf.Clamp(knockbackTotal, (-10 * speed) / rb.mass, 0);
            if (knockbackTotal >= 0) {
                gotHit = false;
                knockbackTotal = 0;
            }
        }

        if(target.position.x < transform.position.x)
        {
            sprite.flipX = true;
            if (selectedHair != null)
                selectedHair.GetComponent<SpriteRenderer>().flipX = true;   
        }
        else
        {
            sprite.flipX = false;
            if (selectedHair != null)
                selectedHair.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Attack()
    {
        if(lastAttackTime + lastCooldownTime < Time.time && attacking)
        {
            Debug.Log("kapow");
            target.GetComponent<Health>().TakeDamage(damage, shakePower, rotPower);
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

        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack") {
            myHealth.TakeDamage(collision.GetComponent<BulletScript>().damage, shakePower, rotPower);
            // add knockback here
            knockbackTotal += collision.GetComponent<BulletScript>().knockback;
            gotHit = true;

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
