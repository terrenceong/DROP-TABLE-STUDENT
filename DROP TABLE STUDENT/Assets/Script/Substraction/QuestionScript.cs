using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionScript : MonoBehaviour
{
    public Text questionText;
    public static int number1,number2;
    public static bool advancedQuestions = false;
    void Awake(){
        generateQuestion();
        questionText.text = "What is " + number1 + " - " + number2 + "?";
    }

    void Update()
    {
      
    }
    public void generateQuestion(){
        if(advancedQuestions){
            do{
                number1 = Random.Range(20, 50);
                number2 = Random.Range(20, 50);
            }
            while(number1<=number2 || (number1-number2)>10 || (number1-number2)<0);
        }
        else{
            do{
                number1 = Random.Range(0, 9);
                number2 = Random.Range(0, 9);
            }
            while(number1<=number2);
        }
    }
}
