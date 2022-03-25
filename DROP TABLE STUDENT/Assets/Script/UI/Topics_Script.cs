using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace Topics
{
    public class Topics_Script : MonoBehaviour
    {
        public static bool add, sub, mul, div;
        public Transform border1, border2;
        public Transform FinalBoss;
        public static int[] Boss_Prereq = new int[8];
        public static int selectedChar = 0; // 0 or 1 base on character selection
                                            // Start is called before the first frame update

        void Start()
        {
            border1.GetComponent<Image>().enabled = false;
            border2.GetComponent<Image>().enabled = false;
            GameMgr.CharIndex = selectedChar; // 0 OR 1 base on selected character Subtraction Mgr
            AddtionGameMgr.CharIndex = selectedChar; // Addtion Mgr
            CharacterManager.CharIndex = selectedChar; // Division Mgr
            MultiplicationMgr.CharIndex = selectedChar;

            /*FinalBoss = GameObject.Find("Final Boss");
            for (int i = 0; i < 7; i++){
                if (Boss_Prereq[i] == 0){
                    FinalBoss.SetActive(false);
                    break;
                }
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            GameMgr.CharIndex = selectedChar; // 0 OR 1 base on selected character Subtraction Mgr
            AddtionGameMgr.CharIndex = selectedChar; // Addtion Mgr
            CharacterManager.CharIndex = selectedChar; // Division Mgr
            MultiplicationMgr.CharIndex = selectedChar;
            if (selectedChar == 0)
            {
                border2.GetComponent<Image>().enabled = false;
                border1.GetComponent<Image>().enabled = true;
            }
            else
            {
                border1.GetComponent<Image>().enabled = false;
                border2.GetComponent<Image>().enabled = true;
            }

        }

        public void addBtn()
        {
            SceneManager.LoadScene("Addition");
        }

        public void subBtn()
        {
            SceneManager.LoadScene("Subtraction");
        }

        public void mulBtn()
        {
            MultiplierGame.difficulty = 0;
            SceneManager.LoadScene("Multiplication");
        }

        public void divBtn()
        {
            //div = true;
            SceneManager.LoadScene("Division");
        }

        public void bossBtn()
        {
            SceneManager.LoadScene("QuizScene");
        }

        public void LeaderboardsBtn()
        {
            SceneManager.LoadScene("Leaderboards");
        }

        public void logOutBtn()
        {
            SceneManager.LoadScene("Login_Screen");
        }

        public void img0()
        {
            selectedChar = 0;
            print("Character 0 selected");
            border2.GetComponent<Image>().enabled = false;
            border1.GetComponent<Image>().enabled = true;

        }

        public void img1()
        {
            selectedChar = 1;
            print("Character 1 selected");
            border1.GetComponent<Image>().enabled = false;
            border2.GetComponent<Image>().enabled = true;

        }
    }
}