using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    private Text timerTextObject;
    public float duration = 30;

    private float timeRemaining;
    private bool timerRunning = false;

    private void Start()
    {
        EventManager.instance.onStartLevel.AddListener(startTimer);

        timerTextObject = gameObject.GetComponent<Text>();
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
        timerTextObject.color = Color.cyan;
        Debug.Log(String.Format("Timer: Starting timer for {0} seconds.", (int)Math.Round(timeRemaining)));
    }

    public void displayTimer(int time){
        timerTextObject.text = String.Format("{0:00}:{1:00}", time/60, time%60);
        if (time <= 10){
            timerTextObject.color = Color.red;
        }
    }
    
}
