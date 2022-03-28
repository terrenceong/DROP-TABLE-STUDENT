using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float duration = 30;

    private float timeRemaining;
    private bool timerRunning = false;

    private void Start()
    {
        EventManager.instance.onRestartLevel.AddListener(startTimer);
        EventManager.instance.onNextLevel.AddListener(startTimer);

        startTimer();
    }

    void Update()
    {
        if (timerRunning) {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                displayTimer((int)Math.Round(timeRemaining));
            } else {
                timeRemaining = 0;
                timerRunning = false;
                Debug.Log("Timer: Timeout.");
                EventManager.instance.timeout();
            }
        }
    } 

    public void startTimer(){
        timeRemaining = duration;
        timerRunning = true;
        Debug.Log(String.Format("Timer: Starting timer for {0} seconds.", (int)Math.Round(timeRemaining)));
    }

    public void displayTimer(int time){
        gameObject.GetComponent<Text>().text = String.Format("{0:00}:{1:00}", time/60, time%60);
        if (time <= 10){
            gameObject.GetComponent<Text>().color = Color.red;
        }
    }
    
}
