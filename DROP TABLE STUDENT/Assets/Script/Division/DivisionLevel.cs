using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DivisionLevel : MonoBehaviour{

    private System.Random randomiser = new System.Random();

    public int score = 0;
    public int passingScore = 10;
    public static int levelNo = 1;
    private int factorCap;
    private int productCap;
    private bool actualAnswer;

    public Text scoreText;
    public Text questionText;
    public Text answerText;
    

    void Start() {
        // add event actions
        EventManager.instance.onSwipeLeft.AddListener(getTrueAnswer);
        EventManager.instance.onSwipeRight.AddListener(getFalseAnswer);
        EventManager.instance.onTimeout.AddListener(getLevelResult);

        if (levelNo.Equals(1)) factorCap = 10;
        else factorCap = 12;
        productCap = factorCap * factorCap;

        generateNewQuestion();
        
    }

    public void getTrueAnswer(){
        checkAnswer(true);
        generateNewQuestion();
    }

    public void getFalseAnswer(){
        checkAnswer(false);
        generateNewQuestion();
    }
    
    private void checkAnswer(bool userAnswer){
        Debug.Log("User's answer is " + userAnswer.ToString());
        Debug.Log("Actual answer is " + actualAnswer.ToString());
        if (userAnswer == actualAnswer){
            Debug.Log("User is correct.");
            EventManager.instance.correct();
            incScore();
        } else {
            Debug.Log("User is wrong.");
            EventManager.instance.wrong();
            decScore();
        }
    }

    private void incScore(){
        scoreText.text = String.Format("Score\n{0}", ++score);
        Debug.Log(String.Format("Increase score to {0}.", score));
    }

    private void decScore(){
        scoreText.text = String.Format("Score\n{0}", --score);
        Debug.Log(String.Format("Decrease score to {0}.", score));
    }

    private void generateNewQuestion(){
        int factor1 = randomiser.Next(1, factorCap+1);
        int factor2 = randomiser.Next(1, factorCap+1);
        int product;
        
        if (randomiser.Next(2) == 1){
            actualAnswer = true;
            product = factor1 * factor2;
        } else{
            actualAnswer = false;
            product = randomiser.Next(factor1, productCap+1);
        }

        // set question text
        questionText.text = String.Format("{0} \u00F7 {1}", product, factor1);
        answerText.text = factor2.ToString();
    }

    private void getLevelResult(){
        bool passed = (score >= passingScore) ? true : false;
        EventManager.instance.result(passed, score);
    }
}
