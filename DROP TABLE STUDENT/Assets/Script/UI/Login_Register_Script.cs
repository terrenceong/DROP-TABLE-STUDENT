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
    public static string username = "dummy";
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
        // username = usernameTxt.GetComponent<InputField>().text;
        // password = passwordTxt.GetComponent<InputField>().text;
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

        if (www.responseCode == 401)
            loginStatus.text = www.downloadHandler.text;
        else if (www.responseCode == 200)
            SceneManager.LoadScene("Topic_Chara_Selection");
        else
            loginStatus.text = www.error;
    }

    public void LoginButton()
    {
        username = usernameTxt.GetComponent<InputField>().text;
        password = passwordTxt.GetComponent<InputField>().text;
        StartCoroutine(Login());
    }

    public void GuestLoginButton()
    {
        username = "Guest_" + UnityEngine.Random.Range(10000, 99999);
        SceneManager.LoadScene("Topic_Chara_Selection");
    }

    public void RegisterButton()
    {
        SceneManager.LoadScene("Registration_Screen");
    }
}
