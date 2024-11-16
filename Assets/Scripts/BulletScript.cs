using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 5;
    public int enemiesToPass = 0;
    public float maxLife = 3.0f;
    public float speed = 0.5f;
    public float area = 1;
    public bool infinitePass = false;

    public virtual void SetOwnStats(float[] myNumStats, bool isInfinite) {
        damage = myNumStats[0];
        enemiesToPass = (int)myNumStats[1];
        maxLife = myNumStats[2];
        speed = myNumStats[3];
        area = myNumStats[4];
        infinitePass = isInfinite;
        gameObject.transform.localScale = transform.localScale * area;
    }
}
