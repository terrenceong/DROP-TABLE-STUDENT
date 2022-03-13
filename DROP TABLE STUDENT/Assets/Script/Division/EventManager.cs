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
        // move Player into result popup
        GameObject player = GameObject.FindWithTag("Player");
        GameObject resultPopup = GameObject.Find("Result Popup Player");
        RectTransform playerRectTransform = player.GetComponent<RectTransform>();
        playerRectTransform.anchorMin = new Vector2(0, 0);
        playerRectTransform.anchorMax = new Vector2(1, 1);
        player.transform.SetParent(resultPopup.transform, false);
        
        onResult?.Invoke(passed, score);
    }

    public UnityEvent onCorrect;
    public void correct(){
        onCorrect?.Invoke();
    }

    public UnityEvent onWrong;
    public void wrong(){
        onWrong?.Invoke();
    }
}
