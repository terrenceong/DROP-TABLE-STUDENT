using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr instance;
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

        if (scene.name == "Subtraction")
        {
           if(GameObject.FindWithTag("Player") == null)
            {
                characters[_charIndex].transform.position = new Vector3(0, 0, 0);
                Instantiate(characters[_charIndex]);
            }
                
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
