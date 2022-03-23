using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

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

    private void ClearLeaderboards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject scoreObject = GameObject.Find("Score" + i);
            scoreObject.GetComponent<Text>().text = "-";
        }
    }

    private void UpdateLeaderboards(string response)
    {
        ClearLeaderboards();

        if (response.Length == 0)
            return;

        string[] scores = response.Split(new[] { "__separator__" }, StringSplitOptions.None);

        for (int i = 0; i < scores.Length; i++)
        {
            GameObject scoreObject = GameObject.Find("Score" + i);
            scoreObject.GetComponent<Text>().text = scores[i];
        }
    }
}
