using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMove;
    public Health playerHealth;
    float xp = 0;
    int levelXp = 5;
    public int level = 1;

    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerMove.inputActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "xpSmall") {
            xp++;
            Destroy(collision.gameObject);
        }
        // since you only get xp when you grab a gem, only need to check once you hit a trigger.
        if (xp >= levelXp) {
            // remove the xp needed to level up, then double the needed xp to level up.
            xp -= levelXp;
            levelXp *= 2;
            LevelUp();

        }
    }

    private void LevelUp() {
        level++;
        Debug.Log("Leveled up to level " + level);
    }
}
