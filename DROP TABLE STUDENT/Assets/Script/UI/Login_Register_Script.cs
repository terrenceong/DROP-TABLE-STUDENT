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
    private string username;
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

        using (UnityWebRequest www = UnityWebRequest.Post(host, form))
        {
            yield return www.SendWebRequest();
            if (www.responseCode == 200)
                SceneManager.LoadScene("Topic_Chara_Selection");
            else if (www.responseCode == 401)
            {
                Debug.Log(www.downloadHandler.text);
                TMP_Text loginStatus = loginStatusLbl.GetComponent<TMP_Text>();
                loginStatus.text = www.downloadHandler.text;
            }
        }
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
