using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MsgController : MonoBehaviour
{
    [SerializeField] private Text GameEnd;
    GameObject TutorialPopup;
    public static bool start = false;

    void Start()
    {
        TutorialPopup=GameObject.Find("Tutorial Popup");
        if(AnswerStatus.readInst)
        {
            TutorialPopup.SetActive(false);
            StopWatch sw = GameObject.FindObjectOfType(typeof(StopWatch)) as StopWatch;
            sw.startTime();
        }
        AnswerStatus.setAns1(0);
        AnswerStatus.setAns2(0);
        AnswerStatus.setAns3(0);
        AnswerStatus.correct = -1;  
    }

    IEnumerator changeSceneAdd(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(AnswerStatus.level >= 2)
        {
            AnswerStatus.level = 1;
            float score = AnswerStatus.timing1 + AnswerStatus.timing2;
            Leaderboards.UploadScore(0, score);
            SceneManager.LoadScene("Topic_Chara_Selection");
        }   
        else
        {
            CircleScript cir = GameObject.FindObjectOfType(typeof(CircleScript)) as CircleScript;
            cir.created = false;
            AnswerStatus.level+=1;
            SceneManager.LoadScene("Addition");
        }
    }

    IEnumerator wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    void Update()
    {
        if(AnswerStatus.correct == 1)
        {
            GameEnd.GetComponent<Text>().text = "WELL DONE!";
            GameEnd.GetComponent<Text>().fontSize = 250;
            GameEnd.GetComponent<Text>().color = Color.green;
            GameEnd.GetComponent<Text>().enabled = true;
            StartCoroutine(changeSceneAdd(1));
        }
        else if(AnswerStatus.correct == 0)
        {
            GameEnd.GetComponent<Text>().text = "Try Again!";
            GameEnd.GetComponent<Text>().fontSize = 250;
            GameEnd.GetComponent<Text>().color = Color.red;
            GameEnd.GetComponent<Text>().enabled = true;
        }
    }

    public void displayLevel()
    {
        GameEnd.GetComponent<Text>().text = ("Level "+AnswerStatus.level);
        GameEnd.GetComponent<Text>().fontSize = 250;
        GameEnd.GetComponent<Text>().color = Color.white;
        GameEnd.GetComponent<Text>().enabled = true;
    }

    public void HidePopup()
    {
        TutorialPopup.SetActive(false);
        AnswerStatus.readInst = true;
        StopWatch sw = GameObject.FindObjectOfType(typeof(StopWatch)) as StopWatch;
        sw.startTime();
    }
}
