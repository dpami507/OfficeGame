using UnityEngine;

public class WaterEnemyScript : EnemyAI
{
    Rigidbody2D rb;

    public GameObject dieParticle;

    bool attacking;

    public GameObject waterPivot;
    public ParticleSystem waterParticle;

    private void Start()
    {
        target = FindFirstObjectByType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        manager = FindFirstObjectByType<GameManager>();
    }

    public override void FixedUpdate()
    {
        Movement();
        PivotSpout();
    }

    void PivotSpout()
    {
        Vector2 posDiff = transform.position - target.transform.position;
        Vector2 direction = posDiff.normalized;
        float rotAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Quaternion rot = Quaternion.Euler(0, 0, rotAngle + 180);
        waterPivot.transform.rotation = Quaternion.Lerp(transform.rotation, rot, speed * Time.time);
    }

    void Movement()
    {
        tDist = Vector2.Distance(transform.position, target.position);

        // movement
        if (!moving && !gotHit) { rb.linearVelocity = Vector2.zero; return; }

        float xDist = (target.position.x - transform.position.x) / tDist;
        float yDist = (target.position.y - transform.position.y) / tDist;

        rb.linearVelocity = new Vector2(xDist * speed, yDist * speed);
        // do knockback here
        if (gotHit)
        {
            rb.linearVelocity *= knockbackTotal;
            rb.linearVelocity = new Vector2(xDist * 4 * knockbackTotal, yDist * 4 * knockbackTotal);
            knockbackTotal += 0.1f;
            if (knockbackTotal >= 0)
            {
                gotHit = false;
                knockbackTotal = 0;
            }
        }
    }

    void Attack()
    {
        lastAttackTime += Time.deltaTime;
        if (lastAttackTime > lastCooldownTime)
        {
            lastAttackTime = 0;
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
        Destroy(Instantiate(dieParticle, transform.position, Quaternion.identity), 1f);

        FindAnyObjectByType<WaveSpawner>().spawnedEnemies.Remove(this.gameObject);

        FindFirstObjectByType<DeathScreenManager>().kills++;

        Destroy(this.gameObject);
    }
}
