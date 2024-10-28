using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public int moveSpeed = 5;
    public int jumpForce = 10;

    public bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    public float extraGravity;

    public ParticleSystem jumpParticles;

    public int yLevel;
    public Vector3 backPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 boxCastSize = new Vector2(GetComponent<BoxCollider2D>().size.x - .025f, 0.05f);
        
        isGrounded = Physics2D.BoxCast(groundCheck.position, boxCastSize, 0, Vector2.zero, 1, groundLayer);
        
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        transform.localScale = (rb.velocity.x < 0) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        
        if (Input.GetButton("Jump") && isGrounded)
        {
            Destroy(Instantiate(jumpParticles, groundCheck.position, Quaternion.identity).gameObject, 1f);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        if (transform.position.y < yLevel)
        {
            Die();
        }
        
        rb.velocity = new Vector2(x,rb.velocity.y);
        
        rb.AddForce(Vector2.down * extraGravity, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        transform.position = backPos;
        rb.velocity = Vector2.zero;
    }
}
