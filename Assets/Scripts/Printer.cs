using UnityEngine;

public class Printer : MonoBehaviour
{
    PlayerManager playerManager;
    Rigidbody2D rb;
    public int upwardsForce;
    public int sidewaysForce;
    int facing;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = FindObjectOfType<PlayerManager>();

        facing = (playerManager.facingLeft) ? 1 : -1;

        rb.linearVelocity = new Vector2(sidewaysForce * facing, upwardsForce);
    }
}
