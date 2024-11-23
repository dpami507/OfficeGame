using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 5;
    public int enemiesToPass = 0;
    public float maxLife = 3.0f;
    public float speed = 0.5f;
    public float area = 1;
    public bool infinitePass = false;

    public bool gameRunning;
    GameManager manager;
    Vector3 storedVel;
    float storedGrvavity;
    Rigidbody2D _rb;

    void Start()
    {
        gameRunning = true;
        manager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (gameRunning != manager.gameRunning)
        {
            _rb = GetComponent<Rigidbody2D>();

            if (manager.gameRunning)
            {
                Debug.Log("Resumed");
                gameRunning = true;
                _rb.gravityScale = storedGrvavity;
                _rb.linearVelocity = storedVel;
            }
            else
            {
                gameRunning = false;
                storedVel = _rb.linearVelocity;
                storedGrvavity = _rb.gravityScale;
                _rb.linearVelocity = Vector3.zero;
                _rb.gravityScale = 0;
                Debug.Log("Paused");
            }
        }
    }

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
