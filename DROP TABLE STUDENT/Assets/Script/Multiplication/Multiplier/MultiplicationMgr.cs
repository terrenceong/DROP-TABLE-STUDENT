using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplicationMgr : MonoBehaviour
{
    public static MultiplicationMgr instance;
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

        if (scene.name == "Multiplication")
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                GameObject sprite = Instantiate(characters[_charIndex], new Vector3(-14, -3, 1), new Quaternion());
                Transform spriteTransform = sprite.GetComponent<Transform>();
                spriteTransform.localScale = new Vector3(-5, 5, 1);
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
