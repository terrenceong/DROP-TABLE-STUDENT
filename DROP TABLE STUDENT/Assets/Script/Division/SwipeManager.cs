using System;
using UnityEngine;
using UnityEngine.EventSystems;
//using Utilities; 

// https://stackoverflow.com/questions/41491765/detect-swipe-gesture-direction
public class SwipeManager : MonoBehaviour {

  private bool swipeEnabled = true;

  public float swipeThreshold = 40f;
  public float timeThreshold = 0.3f;

  private Vector2 _fingerDown;
  private DateTime _fingerDownTime;
  private Vector2 _fingerUp;
  private DateTime _fingerUpTime;

  void Start(){
    EventManager.instance.onTimeout.AddListener(disable);
    EventManager.instance.onRestartLevel.AddListener(enable);
    EventManager.instance.onNextLevel.AddListener(enable);
  }

  private void Update () {
    if (swipeEnabled){
      if (Input.GetMouseButtonDown(0)) {
        _fingerDown = Input.mousePosition;
        _fingerUp = Input.mousePosition;
        _fingerDownTime = DateTime.Now;
      }

      if (Input.GetMouseButtonUp(0)) {
        _fingerDown = Input.mousePosition;
        _fingerUpTime = DateTime.Now;
        CheckSwipe();
      }

      foreach (var touch in Input.touches) {
        if (touch.phase == TouchPhase.Began) {
          _fingerDown = touch.position;
          _fingerUp = touch.position;
          _fingerDownTime = DateTime.Now;
        }

        if (touch.phase == TouchPhase.Ended) {
          _fingerDown = touch.position;
          _fingerUpTime = DateTime.Now;
          CheckSwipe();
        }
      }
    }
  }

  private void CheckSwipe() {
    var duration = (float)_fingerUpTime.Subtract(_fingerDownTime).TotalSeconds;
    var dirVector = _fingerUp - _fingerDown;

    if (duration > timeThreshold) return;
    if (dirVector.magnitude < swipeThreshold) return;

    var direction = Vector2.SignedAngle(Vector2.up, dirVector);

    Debug.Log(String.Format("SwipeManager: Swipe direction angle is {0} degrees.", direction));

    
    if (direction > 0) {
        Debug.Log("SwipeManager: Swipe right detected.");
        EventManager.instance.swipeRight();
    }
    else if (direction < 0) {
        Debug.Log("SwipeManager: Swipe left detected.");
        EventManager.instance.swipeLeft();
    }
  }

  private void enable(){
    Debug.Log("SwipeManager: Swipe manager enabled.");
    swipeEnabled = true;
  }
  private void disable(){
    Debug.Log("SwipeManager: Swipe manager disabled.");
    swipeEnabled = false;
  }
}