using System.Collections;
using UnityEngine;

public class BackgroundPrefab : MonoBehaviour
{
    int destroyTime = 15;
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 1f);
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(Instantiate(explosion, transform.position, transform.rotation), 1f);
        Destroy(this.gameObject);
    }
}
