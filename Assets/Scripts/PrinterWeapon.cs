using System.Collections;
using UnityEngine;

public class PrinterWeapon : WeaponBaseScript
{
    public float upwardsForce = 8;
    public float sidewaysForce = 5;
    public override void Attack(int attackNumber)
    {
        //StartCoroutine(Wait(attackNumber));
        GameObject printer = Instantiate(bullet, transform.position, Quaternion.identity);
        printer.GetComponent<PrinterScript>().damage = damage * damageUpgrade;
        printer.GetComponent<PrinterScript>().enemiesToPass = enemiesToPass;
        PlayerManager playerManager;
        Rigidbody2D rb;
        int facing;
        rb = printer.GetComponent<Rigidbody2D>();
        playerManager = GetComponentInParent<PlayerManager>();
        facing = (playerManager.facingLeft) ? 1 : -1;
        // made it so if the player is moving upward it throws it a little higher
        rb.linearVelocity = new Vector2(sidewaysForce * facing, upwardsForce + (3 * Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0, 1)));
    }

    public override string LevelDescription(int level)
    {
        switch (level)
        {
            case 1:
                return "High damage, high Area scaling.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Base Damage up by 20.";
            case 4:
                return "Passes through 2 more enemies.";
            case 5:
                return "Fires 1 more projectile.";
            case 6:
                return "Base Damage up by 20.";
            case 7:
                return "Passes through 2 more enemies.";
            case 8:
                return "Base Damage up by 20.";
            default:
                return "Error";
        }
    }

    public override void LevelSelfUp(int level)
    {
        switch (level)
        {
            case 2:
                numAttacks++;
                break;
            case 3:
                damage += 10;
                break;
            case 4:
                enemiesToPass += 2;
                break;
            case 5:
                numAttacks++;
                break;
            case 6:
                damage += 10;
                break;
            case 7:
                enemiesToPass += 2;
                break;
            case 8:
                damage += 10;
                break;
            default:
                gameObject.SetActive(true);
                break;
        }
    }
}
