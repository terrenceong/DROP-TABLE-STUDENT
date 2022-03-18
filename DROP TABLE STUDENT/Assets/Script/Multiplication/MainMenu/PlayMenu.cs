using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    private Slider difficultySlider;

    public static int difficulty;

    public void PlayMultiplier()
    {
        SetDifficulty();
        SceneManager.LoadScene(1);
    }

    private void SetDifficulty()
    {
        difficultySlider = GameObject.Find("DifficultySlider").GetComponent<Slider>();
        difficulty = (int)difficultySlider.value;
    }
}
