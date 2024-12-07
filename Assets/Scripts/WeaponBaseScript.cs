using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class WeaponBaseScript : MonoBehaviour
{
    public float lastAttack;
    public WaveSpawner enemies;
    public GameObject bullet;
    public int level = 1;
    public string nameWeapon;
    public string[] levelUpDecriptions;
    // base weapon
    public float attackCooldown;
    public int numAttacks = 1;
    public float damage;
    public bool infinitePass = false;
    public int enemiesToPass = 0;
    public float area = 1;
    public float duration = 3;
    public float speed = 0.5f;
    public float knockback = -0.1f;

    // upgrades
    public float cooldownUpgrade = 1.0f;
    public int numExtraAttacks = 0;
    public float damageUpgrade = 1.0f;
    public float areaUpgrade = 1.0f;
    public float durationUpgrade = 1.0f;
    public float speedUpgrade = 1.0f;

    public bool active;

    private void Start()
    {
        enemies = FindFirstObjectByType<WaveSpawner>();
        active = true;
        if (gameObject.activeSelf == true) {
            level = 2;
        }
    }

    // is virtual to allow it to use the subclass's attack function without having to add a seperate update function there
    public virtual void Update()
    {

        lastAttack += Time.deltaTime;
        if (lastAttack > attackCooldown * cooldownUpgrade && active)
        {
            lastAttack = 0;
            if (nameWeapon != "Paper Airplane")
            {
                StartCoroutine(ShootWithDelay());
            }
            else {
                Attack();
            }
        }
    }
    public virtual void Attack() {
        Debug.Log("Attack function was not overrriden");
    }

    public virtual string LevelDescription(int level) {
        return "error";
    }

    public virtual void LevelSelfUp(int level) { }

    // 0. Max Health
     // 1. Move Speed
     // 2. Strength
     // 3. Duration
     //4. Amount
    // 5. Cooldown
     // 6. Growth
     // 7. Magnet
     // 8. Weapon Speed
     // 9. Area
    public void UpdateStats(float[] statsArray)
    {
        damageUpgrade = statsArray[2];
        durationUpgrade = statsArray[3];
        numExtraAttacks = (int)statsArray[4];
        cooldownUpgrade = statsArray[5];
        speedUpgrade = statsArray[8];
        areaUpgrade = statsArray[9];
}


    public IEnumerator Wait(int number)
    {
        yield return new WaitForSecondsRealtime(0.1f * number);
    }

    public IEnumerator ShootWithDelay() {
        int totalAttacks = numAttacks + numExtraAttacks;
        for (int i = 0; i < totalAttacks; i++)
        {
            //Debug.Log("Shooting " + i);
            Attack();
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
