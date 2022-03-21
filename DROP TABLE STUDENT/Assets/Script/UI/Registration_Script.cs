using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Registration_Script : MonoBehaviour
{
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
        print(username);
        print(password);
        print(cfmPassword);
        if (password == cfmPassword)
            StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(host, form))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
        }
    }

    public void backBtn()
    {
        SceneManager.LoadScene("Login_Screen");
    }
}
