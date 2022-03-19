using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MultiplierGame : MonoBehaviour
{
    [SerializeField]
    private GameObject timerText;
    private static int time = 0;
    public static bool running = true;
    public static int difficulty = 0;

    private void Start()
    {
        time = 0;
        GridManager.answered = 0;
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }


    /// <summary>
    /// Runs this function every second, increments timer
    /// </summary>
    private void UpdateTimer()
    {
        if (running)
        {
            TMP_Text tmpTxt = timerText.GetComponent<TMP_Text>();

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
    public static void EndGame()
    {
        print(time);
        running = false;
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
