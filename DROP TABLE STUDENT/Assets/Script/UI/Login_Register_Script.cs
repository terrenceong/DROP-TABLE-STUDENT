using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

public class Login_Register_Script : MonoBehaviour
{
    public GameObject loginStatusLbl;
    public GameObject usernameTxt;
    public GameObject passwordTxt;
    public static string username = "give me a name";
    private string password;
    private string form;
    private string host = "172.21.148.170/login";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        username = usernameTxt.GetComponent<InputField>().text;
        password = passwordTxt.GetComponent<InputField>().text;
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(host, form);
        www.timeout = 3;

        yield return www.SendWebRequest();

        TMP_Text loginStatus = loginStatusLbl.GetComponent<TMP_Text>();

        if (www.result != UnityWebRequest.Result.Success)
            loginStatus.text = "Couldn't reach server";
        else if (www.responseCode == 401)
            loginStatus.text = www.downloadHandler.text;
        else
            SceneManager.LoadScene("Topic_Chara_Selection");
    }

    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    public void RegisterButton()
    {
        SceneManager.LoadScene("Registration_Screen");
    }
}
