using System;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    float startTime;
    float gameTime;
    public TMP_Text timerTxt;

    private void Start()
    {
        startTime = Time.time;
    }


    void Update()
    {
        if(FindFirstObjectByType<GameManager>().gameRunning == true)
        {
            gameTime = gameTime + Time.deltaTime - startTime;

            string mins = ((int)gameTime / 60).ToString();
            string sec = (gameTime % 60).ToString("f2");

            timerTxt.text = mins + ":" + sec;
        }
    }
}
