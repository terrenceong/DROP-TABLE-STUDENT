using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login_Register_Script : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    private string form;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }

    public void LoginButton(){
        SceneManager.LoadScene("Topic_Chara_Selection");
    }

    public void RegisterButton(){
        SceneManager.LoadScene("Registration_Screen");
    }
}
