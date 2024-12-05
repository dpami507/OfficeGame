using System;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float startTime;
    public float gameTime;
    public TMP_Text timerTxt;

    private void Start()
    {
        startTime = Time.time;
    }


    void Update()
    {
        if(Time.timeScale == 1 && !FindFirstObjectByType<PlayerManager>().dead)
        {
            gameTime = gameTime + Time.deltaTime;

            string mins = ((int)gameTime / 60).ToString("00");
            string sec = ((int)(gameTime % 60)).ToString("00");
            timerTxt.text = mins + ":" + sec;
        }
    }
}
