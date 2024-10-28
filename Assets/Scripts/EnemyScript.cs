using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int speed;
    Transform target;

    Rigidbody2D rb;

    void Start()
    {
        target = FindObjectOfType<PlayerMovementScript>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);

        rb.velocity = transform.up * -speed;
    }
}
