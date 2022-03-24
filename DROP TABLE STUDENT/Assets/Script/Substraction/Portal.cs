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
    GameObject TutorialPopup;
    public static float cdTimer = 60.0f;
    public Text timeStamp;
    private static bool start = false;
    private static bool tutorialRead = false;
    private static bool complete = false;


    private void Update()
    {
        if(cdTimer > 0 && start)
        {
            cdTimer -= Time.deltaTime;
        }
        double b = System.Math.Round(cdTimer, 0);
        timeStamp.text = b + "s";
        if(cdTimer <=0 && !complete)
        {
            complete = true;
        }
        if(complete && cdTimer<=0)
        {
           /*if(streakShownValue > 0)
            {
                Debug.Log(streakShownValue);
                Leaderboards.UploadScore(1, streakShownValue);
            }*/
               
           // validateText.text = "Times up!";
            //validateText.GetComponent<Text>().enabled = true;
            StartCoroutine(waitForSec(false, complete));
         
        }

    }
    private void Awake()
    {
     
        validateText.GetComponent<Text>().enabled = false;
        streakText.text = "" + (streakShownValue);
    }
    void Start()
    {
        TutorialPopup=GameObject.Find("Tutorial Popup");
        if(tutorialRead){
            TutorialPopup.SetActive(false);
        }
    }

    // Update is called once per frame
    IEnumerator waitForSec(bool correct,bool complete)
    {
        if (correct && !complete)
        {
            yield return new WaitForSeconds(1);
            cdTimer += 3;
            SceneManager.LoadScene("Subtraction");
        }
        else if(!complete)
        {
            yield return new WaitForSeconds(1);
            validateText.GetComponent<Text>().enabled = false;
        }
        else
        {

            //yield return new WaitForSeconds(2);
            ReturnMainMenu();


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
                    streakShownValue += 3;
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
                
            }
            else
            {
               
                validateText.text = "Incorrect ! Try Again";
                collectedText.text = "0";
                if(streakShownValue > 0)
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
    public void ReturnMainMenu()
    {
        //Destroy(this.gameObject);
        Debug.Log("return main");
        streakShownValue = 0;
        streakHiddenValue = 0;
        cdTimer = 60.0f;
        complete = false;
        QuestionScript.advancedQuestions = false;
        SceneManager.LoadScene("Topic_Chara_Selection", LoadSceneMode.Single);
    }
    public void HidePopup()
    {
        TutorialPopup.SetActive(false);
        tutorialRead = true;
        start = true;
    }
}
