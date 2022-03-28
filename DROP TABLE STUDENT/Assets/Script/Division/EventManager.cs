using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventResult : UnityEvent<bool, int>{ }

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public UnityEvent onSwipeLeft;
    public void swipeLeft(){
        onSwipeLeft?.Invoke();
    }

    public UnityEvent onSwipeRight;
    public void swipeRight(){
        onSwipeRight?.Invoke();
    }

    public UnityEvent onTimeout;
    public void timeout(){
        Debug.Log("EventManager: Timeout event started.");
        onTimeout?.Invoke();
    }
    
    public UnityEventResult onResult;
    public void result(bool passed, int score){
        Debug.Log("EventManager: Result event started.");
        onResult?.Invoke(passed, score);
    }
    public UnityEvent moveCharacterToResult;

    public UnityEvent onCorrect;
    public void correct(){
        onCorrect?.Invoke();
    }

    public UnityEvent onWrong;
    public void wrong(){
        onWrong?.Invoke();
    }

    public UnityEvent onStartLevel;
    public void startLevel(){
        Debug.Log("EventManager: Start Level event started.");
        onStartLevel?.Invoke();
    }
    public void startNextLevel(){
        Debug.Log("EventManager: Start Next Level event started.");
        DivisionLevel.levelNo++;
        onStartLevel?.Invoke();
    }
}
