using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    private string PLAYER_TAG = "Player";
    [SerializeField]
    private Text collectedText;
    [SerializeField]
    private Text validateText;
    [SerializeField]
    private Text streakText;
    private static int streakHiddenValue = 0;
    private static int streakShownValue = 0;


    // Start is called before the first frame update
    private void Awake()
    {
       
        validateText.GetComponent<Text>().enabled = false;
        streakText.text = "" + (streakShownValue);
    }
    void Start()
    {


    }

    // Update is called once per frame
    IEnumerator waitForSec(bool correct,bool complete)
    {
        if (correct && !complete)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Subtraction");
        }
        else if(complete)
        {
            yield return new WaitForSeconds(1);
            validateText.text = "Stage Completed!";
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Topic_Chara_Selection");

        }
        else
        {
            yield return new WaitForSeconds(1);
            validateText.GetComponent<Text>().enabled = false;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool correct = false;
        bool complete = false;
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {

            //validateText.GetComponent<Text>().enabled = false;
            int answer = QuestionScript.number1 - QuestionScript.number2;
            if (answer == int.Parse(collectedText.text))
            {
                validateText.text = "Correct Well Done!";
                correct = true;
                if (QuestionScript.advancedQuestions == true)
                {
                    streakShownValue += 2;
                }
                else
                {
                    streakShownValue++;
                }

                if (streakHiddenValue >= 2)
                {
                    QuestionScript.advancedQuestions = true;
                }
                if (streakHiddenValue >= 5)
                {
                    complete = true;
                    streakHiddenValue = -1;

                }
                streakHiddenValue++;
                streakText.text = "" + (streakShownValue);
                
            }
            else
            {
                validateText.text = "Incorrect ! Try Again";
                collectedText.text = "0";
                if (streakShownValue > 0)
                {
                    streakShownValue--;
                }
                streakText.text = "" + (streakShownValue);

            }
            validateText.GetComponent<Text>().enabled = true;
            StartCoroutine(waitForSec(correct,complete));


            //validateText.GetComponent<Text>().enabled = false;
            //SceneManager.LoadScene("GamePlay");

            //SceneManager.LoadScene("NextScene");
        }
    }
}
