using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MultiplierGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _timerText;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    public GameObject tutorialPopup;

    private GridManager _gridManager;
    private int defaultScore = 1000;
    private static int time;
    private static bool multTutWatched = false;
    public static bool running;
    public static int difficulty = 0;

    public static MultiplierGame instance;
    [SerializeField]
    private GameObject[] characters;
    public GameObject character = null;
    private static int _charIndex;
    public static int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Start()
    {
        _gridManager = GameObject.Find("GameContainer").GetComponent<GridManager>();

        if (multTutWatched)
            StartGame();
        else
            ShowTutorial();

        // increments timer every second
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }


    /// <summary>
    /// Runs this function every second, increments timer
    /// </summary>
    private void UpdateTimer()
    {
        if (running)
        {
            TMP_Text tmpTxt = _timerText.GetComponent<TMP_Text>();

            time += 1;
            int min = time / 60,
                sec = time % 60;

            string timeStr = string.Format("{0:00}:{1:00}", min, sec);
            tmpTxt.text = timeStr;
        }
    }


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
                character = Instantiate(characters[_charIndex], new Vector3(-7, -1.5f, 1), new Quaternion());
                Transform spriteTransform = character.GetComponent<Transform>();
                spriteTransform.localScale = new Vector3(-2, 2, 1);
            }
        }
    }


    public void StartGame()
    {
        // resets game state on start
        time = 0;
        GridManager.answered = 0;
        running = true;

        TMP_Text tmpTxt = _timerText.GetComponent<TMP_Text>();
        tmpTxt.text = "00:00";

        _gridManager.InitBoard();
        _gridManager.DrawTargets();
    }


    /// <summary>
    /// Stops timer and ends game
    /// </summary>
    public void EndGame()
    {
        int score = CalculateScore();
        Leaderboards.UploadScore(2, score);

        _gameOverText.SetActive(true);
        running = false;

        if (difficulty == 2)
            StartCoroutine(DelayedReturn(2.5f));
        else
        {
            difficulty++;
            StartCoroutine(DelayedReset(2.5f));
        }
    }


    private IEnumerator DelayedReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        _gameOverText.SetActive(false);
        GridManager.ClearAnsLines();
        StartGame();
    }

    private IEnumerator DelayedReturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnMainMenu();
    }


    private int CalculateScore()
    {
        return (difficulty + 1) * (int)Mathf.Max(0f, defaultScore - time);
    }


    public void ShowTutorial()
    {
        tutorialPopup.SetActive(true);
        running = false;
    }

    public void HideTutorial()
    {
        tutorialPopup.SetActive(false);
        multTutWatched = true;
        StartGame();
    }


    /// <summary>
    /// Exits the Multiplier Game scene
    /// </summary>
    public void ReturnMainMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Topic_Chara_Selection", LoadSceneMode.Single);
    }
}
