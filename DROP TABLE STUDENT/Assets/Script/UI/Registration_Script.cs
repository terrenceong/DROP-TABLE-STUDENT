using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Registration_Script : MonoBehaviour
{
    public GameObject username, password, cfm_password;
    private string Username, Password, Cfm_password;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        Cfm_password = cfm_password.GetComponent<InputField>().text;
    }

    public void RegisterButton(){
        print(Username);
        print(Password);
        print(Cfm_password);
        print("Register Button");
    }

    public void backBtn(){
        SceneManager.LoadScene("Login_Screen");
    }
}
