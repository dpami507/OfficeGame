using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMove;
    public Health playerHealth;

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
}
