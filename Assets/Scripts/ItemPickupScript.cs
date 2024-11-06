using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    PlayerManager player;
    private void Start()
    {
        player = GetComponentInParent<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "xpSmall")
        {
            player.xpIncrease(1);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "xpMedium")
        {
            player.xpIncrease(5);
            Destroy(collision.gameObject);
        }
    }
    // when we add an ability that uses this
    public float sizeChange(float valueChange) {
        GetComponent<CircleCollider2D>().radius += valueChange;
        return GetComponent<CircleCollider2D>().radius;
    }
}
