using System.Collections;
using UnityEngine;

public class PrinterWeapon : WeaponBaseScript
{
    public float upwardsForce = 8;
    public float sidewaysForce = 5;
    public override void Attack()
    {
        GameObject printer = Instantiate(bullet, transform.position, Quaternion.identity);
        PlayerManager playerManager;
        Rigidbody2D rb;
        int facing;
        rb = printer.GetComponent<Rigidbody2D>();
        playerManager = GetComponentInParent<PlayerManager>();
        facing = (playerManager.facingLeft) ? 1 : -1;
        // made it so if the player is moving upward it throws it a little higher
        rb.linearVelocity = new Vector2(sidewaysForce * facing, upwardsForce + (3 * Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0, 1)));
    }
}
