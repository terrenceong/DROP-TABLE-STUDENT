using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters;
    // Start is called before the first frame update
    private static int _charIndex;
    public static int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }
    public GameObject character;
    void Start()
    {
        character = Instantiate(characters[_charIndex], this.transform);

        // add event actions
        EventManager.instance.onCorrect.AddListener(correct);
        EventManager.instance.onWrong.AddListener(wrong);
        EventManager.instance.onResult.AddListener(moveToResult);
        EventManager.instance.onStartLevel.AddListener(moveToGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void idle(){
        character.GetComponent<Animator>().SetTrigger("idle");
        Debug.Log("CharacterManager: Playing character correct animation.");
    }

    private void correct()
    {
        character.GetComponent<Animator>().SetTrigger("correct");
        Debug.Log("CharacterManager: Playing character correct animation.");
    }

    private void wrong()
    {
        character.GetComponent<Animator>().SetTrigger("wrong");
        Debug.Log("CharacterManager: Playing character wrong animation.");
    }

    private void win()
    {
        character.GetComponent<Animator>().SetTrigger("win");
        Debug.Log("CharacterManager: Playing character win animation.");
    }

    private void lose()
    {
        character.GetComponent<Animator>().SetTrigger("lose");
        Debug.Log("CharacterManager: Playing character lose animation.");
    }

    private void setResultAnimation(bool passed){
        if (passed) win();
        else lose();
    }

    private void moveToResult(bool passed, int score){
        // move Player into result popup
        GameObject resultPopup = GameObject.Find("Result Popup Player");
        RectTransform playerRectTransform = gameObject.GetComponent<RectTransform>();
        playerRectTransform.anchorMin = new Vector2(0, 0);
        playerRectTransform.anchorMax = new Vector2(1, 1);
        gameObject.transform.SetParent(resultPopup.transform, false);
        // set player animation
        setResultAnimation(passed);
    }

    private void moveToGame(){
        // move Player into result popup
        GameObject game = GameObject.Find("Division Game");
        RectTransform playerRectTransform = gameObject.GetComponent<RectTransform>();
        playerRectTransform.anchorMin = new Vector2(0, 0);
        playerRectTransform.anchorMax = new Vector2(0.4f, 0.75f);
        gameObject.transform.SetParent(game.transform, false);
        // set player animation
        idle();
    }
}
