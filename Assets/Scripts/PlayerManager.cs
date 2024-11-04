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

    // level and experience variables
    float xp = 0;
    public int levelXp = 5;
    public int level = 1;
    public Image xpBar;
    public TMP_Text levelTxt;

    // speed and dashing
    public Vector2 playerVelocity;
    public int speed;
    public int dashSpeed;
    public float dashTime;
    public float dashCooldownTime;
    float lastDashTime;
    bool inputActive;

    // pause screen things
    bool isPaused = false;
    public GameObject levelUI;
    public bool facingLeft;


    private void Start()
    {
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

        //Get Input
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float y = Input.GetAxisRaw("Vertical") * speed;

        //Set Velocity
        playerVelocity = new Vector2(x, y);
        rb.linearVelocity = playerVelocity;
    }

    void Die()
    {
        // add game over screen in the future
        inputActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "xpSmall") {
            xp++;
            Destroy(collision.gameObject);
            UpdateXPBar();
        }
        if (collision.tag == "xpMedium")
        {
            xp += 5;
            Destroy(collision.gameObject);
            UpdateXPBar();
        }
        // since you only get xp when you grab a gem, only need to check once you hit a trigger.
        if (xp >= levelXp) {
            // remove the xp needed to level up, then double the needed xp to level up.
            xp -= levelXp;
            levelXp = Mathf.RoundToInt(Mathf.Floor(level + (50 * Mathf.Pow(2, level / 7f)) - 40));
            LevelUp();
            UpdateXPBar();
        }
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

}