using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Image healthBar;

    public bool canTakeDamage;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) { return; }

        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
