using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace Topics{
    public class Topics_Script : MonoBehaviour
    {
        public static bool add, sub, mul, div;

        public GameObject FinalBoss;
        public static int[] Boss_Prereq = new int[8];
        public static int selectedChar = 0; // 0 or 1 base on character selection
        // Start is called before the first frame update
        void Start()
        {
            GameMgr.CharIndex = selectedChar; // 0 OR 1 base on selected character Subtraction Mgr
            AddtionGameMgr.CharIndex = selectedChar; // Addtion Mgr
            CharacterManager.CharIndex = selectedChar; // Division Mgr

            FinalBoss = GameObject.Find("Final Boss");
            for (int i = 0; i < 7; i++){
                if (Boss_Prereq[i] == 0){
                    FinalBoss.SetActive(false);
                    break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public void addBtn(){
            //add = true;
            SceneManager.LoadScene("Addition");
        }

        public void subBtn(){
            //sub = true;
            SceneManager.LoadScene("Subtraction");
        }

        public void mulBtn(){
            //mul = true;
            //SceneManager.LoadScene("Level1_2");
        }

        public void divBtn(){
            //div = true;
            SceneManager.LoadScene("Division");
        }

        public void bossBtn(){
            print("Why do I hear boss music?");
        }

        public void logOutBtn(){
            SceneManager.LoadScene("Login_Screen");
        }

    }
}