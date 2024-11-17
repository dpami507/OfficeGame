using System.Collections;
using UnityEngine;

public class BackgroundPrefab : MonoBehaviour
{
    int destroyTime = 15;
    public GameObject explosion;

    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 1f);
    }
}
