using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MultiplierGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _timerText;
    [SerializeField]
    private GameObject _gameOverText;
    private int defaultScore = 1000;
    private static int time;
    public static bool running;
    public static int difficulty = 1;

    private void Start()
    {
        // resets game state on start
        time = 0;
        GridManager.answered = 0;
        running = true;

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


    /// <summary>
    /// Stops timer and ends game
    /// </summary>
    public void EndGame()
    {
        int score = CalculateScore();
        Leaderboards.UploadScore(2, score);

        _gameOverText.SetActive(true);
        running = false;
    }

    private int CalculateScore()
    {
        return (difficulty + 1) * (int)Mathf.Max(0f, defaultScore - time);
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
