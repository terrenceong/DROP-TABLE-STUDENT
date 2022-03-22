using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

public class Registration_Script : MonoBehaviour
{
    public GameObject registerStatusLbl;
    public GameObject usernameTxt, passwordTxt, cfmPasswordTxt;
    private string username, password, cfmPassword;
    private string host = "172.21.148.170/register";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        username = usernameTxt.GetComponent<InputField>().text.Trim();
        password = passwordTxt.GetComponent<InputField>().text.Trim();
        cfmPassword = cfmPasswordTxt.GetComponent<InputField>().text.Trim();
    }

    public void RegisterButton()
    {
        if (password == cfmPassword)
            StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(host, form);
        www.timeout = 3;

        yield return www.SendWebRequest();

        TMP_Text registerStatus = registerStatusLbl.GetComponent<TMP_Text>();

        // conflict error
        if (www.responseCode == 409)
        {
            registerStatus.color = Color.red;
            registerStatus.text = www.downloadHandler.text;
        }

        // unreachable
        else if (www.result != UnityWebRequest.Result.Success)
        {
            registerStatus.color = Color.red;
            registerStatus.text = "Couldn't reach server";
        }

        // registered
        else
        {
            registerStatus.color = Color.green;
            registerStatus.text = "Successfully registered!";
        }
    }

    public void backBtn()
    {
        SceneManager.LoadScene("Login_Screen");
    }
}
