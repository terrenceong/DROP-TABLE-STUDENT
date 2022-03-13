using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters;
    public int charIndex;
    // Start is called before the first frame update

    public GameObject character;
    void Start()
    {
        character = Instantiate(characters[charIndex], this.transform);

        // add event actions
        EventManager.instance.onCorrect.AddListener(correct);
        EventManager.instance.onWrong.AddListener(wrong);
        EventManager.instance.onResult.AddListener(result);
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

    public void result(bool passed, int score){
        if (passed) win();
        else lose();
    }
}
