using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerStatus
{
    private static int ans1 = 0;
    private static int ans2 = 0;
    private static int ans3 = 0;
    public static int correct = -1;
    public static int level = 1;
    public static bool readInst = false;
    public static float timing1;
    public static float timing2;

    public static bool allAnswered()
    {
        if(ans1 != 0 && ans2 != 0 && ans3 != 0){
            return true;
        }
        return false;
    }

    public static void setAns1(int ans)
    {
        ans1 = ans;
    }

    public static void setAns2(int ans)
    {
        ans2 = ans;
    }

    public static void setAns3(int ans)
    {
        ans3 = ans;
    }

    public static int getAns1()
    {
        return ans1;
    }
    
    public static int getAns2()
    {
        return ans2;
    }

    public static int getAns3()
    {
        return ans3;
    }

    
}
