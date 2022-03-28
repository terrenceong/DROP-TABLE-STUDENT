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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void correct()
    {
        character.GetComponent<Animator>().SetTrigger("correct");
        Debug.Log("CharacterManager: Playing character correct animation.");
    }

    public void wrong()
    {
        character.GetComponent<Animator>().SetTrigger("wrong");
        Debug.Log("CharacterManager: Playing character wrong animation.");
    }

    public void win()
    {
        character.GetComponent<Animator>().SetTrigger("win");
        Debug.Log("CharacterManager: Playing character win animation.");
    }

    public void lose()
    {
        character.GetComponent<Animator>().SetTrigger("lose");
        Debug.Log("CharacterManager: Playing character lose animation.");
    }

    public void setResultAnimation(bool passed){
        if (passed) win();
        else lose();
    }
}
