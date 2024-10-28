using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float targetScore;
    public ParticleSystem celebrationParticles;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if(scoreManager.score >= targetScore)
        {
            Celebrate();
        }
    }

    public void Celebrate()
    {
        scoreManager.score = 0;
        celebrationParticles.Play();
    }
}
