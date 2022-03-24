using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    float currTime;
    public Text currTimeText;
    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            currTime = currTime + Time.deltaTime;
            currTimeText.text = currTime.ToString();
        }
        else
        {
            Debug.Log(currTime);//To Pass to LeaderBoard
        }
    }

    public void stopTime(){
        isActive = false;
    }

}