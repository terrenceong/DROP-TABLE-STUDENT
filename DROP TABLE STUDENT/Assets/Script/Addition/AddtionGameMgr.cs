using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddtionGameMgr : MonoBehaviour
{
    public static AddtionGameMgr instance;
    [SerializeField]
    private GameObject[] characters;
    private static int _charIndex;
    public static int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Addition")
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                characters[_charIndex].transform.position = new Vector3((float)9.6419, (float)(-3.05), 0);
                Instantiate(characters[_charIndex]);
            }
               
        }

    }

}
