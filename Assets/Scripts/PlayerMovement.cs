using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerVelocity;
    public int speed;

    public int dashSpeed;
    public float dashTime;
    public float dashCooldownTime;
    public float lastDashTime;
    
    Rigidbody2D rb;

    public bool inputActive;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Get Rigidbody
        inputActive = true; //Allow Movement
        lastDashTime = -dashCooldownTime; //Allow Imediate Dash
    }

    private void Update()
    {
        //Update Player facing direction
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector2(1, 1);
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
        Movement();
    }

    void Movement()
    {
        if (!inputActive) { return; }

        //Get Input
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float y = Input.GetAxisRaw("Vertical") * speed;

        //Set Velocity
        playerVelocity = new Vector2(x, y);
        rb.linearVelocity = playerVelocity;
    }

    IEnumerator Dash()
    {
        inputActive = false;

        rb.linearVelocity = (playerVelocity / 5) * dashSpeed;
        yield return new WaitForSeconds(dashTime);

        inputActive = true;
    }
}
