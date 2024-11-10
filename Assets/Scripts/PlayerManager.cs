using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class PlayerManager : MonoBehaviour
{
    // dependencies and refrences
    Health playerHealth;
    Rigidbody2D rb;
    SpriteRenderer mySprite;
    public List<GameObject> weapons;

    // level and experience variables
    float xp = 0;
    public int levelXp = 5;
    public int level = 1;
    public Image xpBar;
    public TMP_Text levelTxt;

    // weapon and trinket trackers

    public int numWeapons = 1;
    public int numTrinkets = 0;
    public Dictionary<string, int> TrinketData = new();

    // speed and dashing
    public Vector2 playerVelocity;
    public int speed;
    public int dashSpeed;
    public float dashTime;
    public float dashCooldownTime;
    float lastDashTime;
    bool inputActive;

    // player stats
    /* potential Stats to add:
     * 0. Max Health
     * 1. Regeneration (per second)
     * 2. Armor 
     * 3. Move Speed
     * 4. Strength
     * 5. Area (size of weapon attacks)
     * 6. Weapon Speed (speed that the weapons move at)
     * 7. Duration (how long weapons can stay on screen)
     * 8. Amount (extra projectiles)
     * 9. Cooldown
     * 10. Luck
     * 11. Growth (extra experience per gem)
     * 12. Greed (affects gold gained)
     * 13. Curse (affects how many enemies spawns and their stats)
     * 14. Magnet (range of pickups)
     */
    // Current added stats in order of index:
    float[] stats = {
        100, // 0. Max Health
        1.0f, // 1. Move Speed
        1.0f, // 2. Strength
        1.0f, // 3. Duration
        0, // 4. Amount
        1.00f, // 5. Cooldown
        1.0f, // 6. Growth
        0.75f // 7. Magnet
    };

    // pause screen things
    bool isPaused = false;
    public GameObject levelUI;
    public bool facingLeft;


    private void Start()
    {
        // Add all trinkets and starting level, maybe add the weapons to this dictionary too?
        TrinketData.Add("Coffee", 1);
        TrinketData.Add("Magnet", 1);
        TrinketData.Add("Sugar Cube", 1);
        TrinketData.Add("Copier", 1);
        TrinketData.Add("Color Ink", 1);

        rb = GetComponent<Rigidbody2D>(); //Get Rigidbody
        playerHealth = GetComponent<Health>(); // set up health
        mySprite = GetComponentInChildren<SpriteRenderer>(); // get access to sprite
        inputActive = true; //Allow Movement
        lastDashTime = -dashCooldownTime; //Allow Imediate Dash
        UpdateXPBar(); //Update XP Bar;
        levelXp = Mathf.RoundToInt(Mathf.Floor(level + (50 * Mathf.Pow(2, level / 7f)) - 40));
    }

    private void Update()
    {
        // check if dead
        if (playerHealth.currentHealth <= 0)
        {
            Die();
        }

        //Update Player facing direction
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            mySprite.flipX = false;
            facingLeft = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            mySprite.flipX = true;
            facingLeft = false;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && lastDashTime + dashCooldownTime < Time.time)
        {
            StartCoroutine(Dash());
            lastDashTime = Time.time;
        }
    }
    private void FixedUpdate()
    {
        // moving
        if (!inputActive || isPaused) { return; }

        //Set Velocity
        playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerVelocity * speed * stats[1];
    }

    void Die()
    {
        // add game over screen in the future
        inputActive = false;
    }

    public void xpIncrease(int amount) {
        xp += amount;
        // since you only get xp when you grab a gem, only need to check once you hit a trigger.
        if (xp >= levelXp)
        {
            do
            {
                // remove the xp needed to level up, then double the needed xp to level up.
                xp -= levelXp;
                levelXp = Mathf.RoundToInt(Mathf.Floor(level + (50 * Mathf.Pow(2, level / 7f)) - 40));
                LevelUp();
                StartCoroutine(Wait());
            } while (xp >= levelXp);
        }
        UpdateXPBar();
    }

    void UpdateXPBar()
    {
        xpBar.fillAmount = xp / (float)levelXp;
        levelTxt.text = level.ToString();
    }

    private void LevelUp() {
        level++;
        Debug.Log("Leveled up to level " + level);
        FindFirstObjectByType<MainMenuUI>().PauseGame(levelUI);
    }

    public string LevelDescription(string item, int level) {
        switch (item)
        {
            case "Pencil":
                return weapons[0].GetComponent<PencilWeapon>().LevelDescription(level);
            case "Printer":
                return weapons[1].GetComponent<PrinterWeapon>().LevelDescription(level);
            case "Coffee":
            case "Magnet":
            case "Sugar Cube":
            case "Copier":
            case "Color Ink":
                return TrinketDescription(item);
            default:
                return "error2";
        }
    }

    private string TrinketDescription(string item) {
        switch (item) {
            case "Coffee":
                return "Increase movement speed by 10%.";
            case "Magnet":
                return "Increase pickup range by 30%.";
            case "Sugar Cube":
                return "Reduce cooldowns by 8%";
            case "Copier":
                return "Shoot an additional projectile.";
            case "Color Ink":
                return "Increase damage by 20%.";
            default:
                return "Error1";
        }
    }

    public void ApplyLevelChoice(string item)
    {
        switch (item)
        {
            case "Pencil":
            case "Printer":
                Debug.Log(item);
                foreach (GameObject weapon in weapons) {
                    if (weapon.GetComponent<WeaponBaseScript>().nameWeapon == item) {
                        weapon.GetComponent<WeaponBaseScript>().LevelSelfUp(weapon.GetComponent<WeaponBaseScript>().level);
                        weapon.GetComponent<WeaponBaseScript>().level++;
                    }
                }

                break;
            case "Coffee":
                Debug.Log(item);
                TrinketData[item]++;
                stats[1] += 0.1f;
                break;
            case "Magnet":
                Debug.Log(item);
                TrinketData[item]++;
                IncreaseMagnetRange();
                break;
            case "Sugar Cube":
                Debug.Log(item);
                TrinketData[item]++;
                stats[5] -= 0.08f;
                break;
            case "Copier":
                Debug.Log(item);
                TrinketData[item]++;
                stats[4] += 1;
                break;
            case "Color Ink":
                Debug.Log(item);
                TrinketData[item]++;
                stats[2] += 0.1f;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<WeaponBaseScript>().UpdateStats(stats);
        }
    }

    void IncreaseMagnetRange()
    {
        stats[7] += 0.5f;
        GetComponentInChildren<CircleCollider2D>().radius = stats[7];
    }

    // coroutines
    IEnumerator Dash()
    {
        Debug.Log("Dashing");

        inputActive = false;
        playerHealth.canTakeDamage = false; //Allow for dashing through enemies

        rb.linearVelocity = (playerVelocity / 5) * dashSpeed;
        yield return new WaitForSeconds(dashTime);

        inputActive = true;
        playerHealth.canTakeDamage = true; //Stop God
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(1);
    }
}