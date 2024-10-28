using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;

    public TMP_Text scoreTxt;

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int x)
    {
        score += x;
        scoreTxt.text = "Score: " + score;
    }
}
