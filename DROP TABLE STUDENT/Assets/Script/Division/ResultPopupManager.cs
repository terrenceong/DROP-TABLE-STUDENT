using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ResultPopupManager : PopupManager
{

    public String  test = "test";
    public Text resultStatusText;
    public Text finalScoreText;
    private GameObject buttonPrefab;
    private const int MaxButtonNo = 2;
    private const float ButtonHoriProp = 1f / (2 * MaxButtonNo + 1); // for n buttons, there are n+1 spaces inbetween. so total intercals is 2n+1.

    protected override void Start(){
        EventManager.instance.onResult.AddListener(pop);

        Debug.Log(String.Format("ResultPopupManager: Horizontal proportion of buttons will be {0}.", ButtonHoriProp));
        buttonPrefab = Resources.Load("Prefabs/Division/Button", typeof(GameObject)) as GameObject;
        if (buttonPrefab == null){
            Debug.Log("ResultPopupManager: Button prefab not found.");
        }
    }

    public void pop(int levelNo, bool passed, int score){
        // set score
        Debug.Log(String.Format("ResultPopupManager: Final score is {0}.", score));
        finalScoreText.text = String.Format("Score\n{0}", score);

        // set result message
        if (passed){
            resultStatusText.text = String.Format("Level {0} Completed!", levelNo);
            resultStatusText.color = Color.green;
            Debug.Log(String.Format("ResultPopupManager: Level {0} passed.", levelNo));
        } else{
            resultStatusText.text = String.Format("Level {0} Failed!", levelNo);;
            resultStatusText.color = Color.red;
            Debug.Log(String.Format("ResultPopupManager: Level {0} failed.", levelNo));
        }

        // set buttons
        GameObject backButton =  createButton("BACK", back);
        setButtons(new GameObject[]{backButton});

        base.pop();
    }

    private GameObject createButton(String buttonText, UnityAction buttonAction){
        GameObject button = (GameObject)Instantiate(buttonPrefab);
        button.GetComponent<Button>().onClick.AddListener(() => back());
        button.transform.GetChild(0).GetComponent<Text>().text = buttonText;
        Debug.Log(String.Format("ResultPopupManager: {0} button created.", buttonText));
        return button;
    }

    private void setButtons(GameObject[] buttons){
        GameObject buttonContainer = GameObject.Find("Result Button Container");
        float spaceHoriProp = (1 - (ButtonHoriProp * buttons.Length)) / (buttons.Length + 1);
        for (int i=0; i<buttons.Length;i++){
            GameObject button = buttons[i];
            RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
            float startX = i * (spaceHoriProp + ButtonHoriProp) + spaceHoriProp;
            float startY = startX + ButtonHoriProp;
            Debug.Log(String.Format("ResultPopupManager: Horizontal anchors for button {0} are {1} and {2}.", i+1, startX, startY));
            buttonRectTransform.anchorMin = new Vector2(startX, 0.25f);
            buttonRectTransform.anchorMax = new Vector2(startY, 0.75f);
            button.transform.SetParent(buttonContainer.transform, false);
            Debug.Log(String.Format("ResultPopupManager: Button {0} set.", i+1));
        }
    }

    public void back(){
        Debug.Log("back clicked");
        SceneManager.LoadScene("Topic_Chara_Selection", LoadSceneMode.Single);
    }

    public void nextLevel(){}
}
