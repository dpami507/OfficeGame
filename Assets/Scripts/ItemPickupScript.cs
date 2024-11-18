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
        if (collision.GetComponent<XP_Script>())
        {
            player.xpIncrease(collision.GetComponent<XP_Script>().xpValue);
            Destroy(collision.gameObject);
            FindFirstObjectByType<SoundManager>().PlaySound("pickup");
        }
    }
    // when we add an ability that uses this
    public float sizeChange(float valueChange) {
        GetComponent<CircleCollider2D>().radius += valueChange;
        return GetComponent<CircleCollider2D>().radius;
    }
}
