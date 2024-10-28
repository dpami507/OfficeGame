using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    ScoreManager scoreManager;
    public int scoreAmount;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            scoreManager.AddScore(scoreAmount);
            Destroy(this.gameObject);
        }
    }
}
