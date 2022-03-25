using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Leaderboards : MonoBehaviour
{
    private static string uploadHost = "172.21.148.170/uploadscore";
    private string leaderboardsHost = "172.21.148.170/leaderboards";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Upload scores by type. Addition=0, Subtraction=1, Multiplication=2, Division=3
    /// </summary>
    /// <param name="gameType"></param>
    /// <param name="score"></param>
    public static void UploadScore(int gameType, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Login_Register_Script.username);
        form.AddField("gameType", gameType);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post(uploadHost, form);
        www.timeout = 3;
        www.SendWebRequest();
    }

    public void ShowAddition()
    {
        StartCoroutine(RetrieveLeaderboards(0));
    }

    public void ShowSubtraction()
    {
        StartCoroutine(RetrieveLeaderboards(1));
    }

    public void ShowMultiplication()
    {
        StartCoroutine(RetrieveLeaderboards(2));
    }

    public void ShowDivision()
    {
        StartCoroutine(RetrieveLeaderboards(3));
    }

    /// <summary>
    /// Retrieves top 5 users by game type and displays them in the leaderboards scene
    /// </summary>
    /// <param name="gameType"></param>
    /// <returns></returns>
    public IEnumerator RetrieveLeaderboards(int gameType)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameType", gameType);

        UnityWebRequest www = UnityWebRequest.Post(leaderboardsHost, form);
        www.timeout = 3;

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            print("Couldn't reach server");
        else if (www.responseCode == 401)
            print(www.downloadHandler.text);
        else
        {
            UpdateLeaderboards(www.downloadHandler.text);
        }
    }

    /// <summary>
    /// Empties the leaderboards, called on any of the 4 button presses
    /// </summary>
    private void ClearLeaderboards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject scoreObject = GameObject.Find("Score" + i);
            scoreObject.GetComponent<Text>().text = "-";
        }
    }

    /// <summary>
    /// Updates the 5 score objects with the users and their scores
    /// </summary>
    /// <param name="response"></param>
    private void UpdateLeaderboards(string response)
    {
        ClearLeaderboards();

        if (response.Length == 0)
            return;

        JArray arr = JArray.Parse(response);

        for (int i = 0; i < arr.Count; i++)
        {
            JToken score = arr[i];
            string username = (string)score["username"];
            string score_val = (string)score["score_value"];

            GameObject scoreObject = GameObject.Find("Score" + i);
            scoreObject.GetComponent<Text>().text = $"{username}: {score_val}";
        }
    }


    public void ReturnMainMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Topic_Chara_Selection", LoadSceneMode.Single);
    }
}
