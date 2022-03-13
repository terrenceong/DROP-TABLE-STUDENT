using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopupManager : PopupManager
{

    public String  test = "test";
    public Text resultStatusText;
    public Text finalScoreText;

    protected override void Start(){
        EventManager.instance.onResult.AddListener(pop);
    }

    public void pop(bool passed, int score){
        Debug.Log(String.Format("ResultPopupManager: Final score is {0}.", score));
        finalScoreText.text = String.Format("Score\n{0}", score);
        if (passed){
            resultStatusText.text = "Level Completed!";
            resultStatusText.color = Color.green;
            Debug.Log("ResultPopupManager: Level passed.");
        } else{
            resultStatusText.text = "Level Failed!";
            resultStatusText.color = Color.red;
            Debug.Log("ResultPopupManager: Level failed.");
        }
        base.pop();
    }
}
