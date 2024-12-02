using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Slider healthBar;

    public bool canTakeDamage;

    public bool isPlayer;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
        healthBar.value = currentHealth / maxHealth;

        if (!isPlayer)
        {
            currentHealth = maxHealth + FindAnyObjectByType<PlayerManager>().level * .2f;
        }
    }

    public void TakeDamage(float damage, float shakeAmount, float rotAmount)
    {
        if (!canTakeDamage) { return; }

        currentHealth -= damage;
        healthBar.value = currentHealth / maxHealth;

        if (isPlayer)
        { 
            FindFirstObjectByType<CameraFollow>().Shake(shakeAmount, rotAmount);
            FindFirstObjectByType<SoundManager>().PlaySound("hit");
        }
    }
}
