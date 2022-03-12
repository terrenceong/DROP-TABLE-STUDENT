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
    IEnumerator waitForSec(bool correct)
    {
        if (correct)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Subtraction");
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
                streakHiddenValue++;
                streakText.text = "" + (streakShownValue);
                // SceneManager.LoadScene("GamePlay");
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
            StartCoroutine(waitForSec(correct));


            //validateText.GetComponent<Text>().enabled = false;
            //SceneManager.LoadScene("GamePlay");

            //SceneManager.LoadScene("NextScene");
        }
    }
}
