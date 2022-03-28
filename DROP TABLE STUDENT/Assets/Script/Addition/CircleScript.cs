using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public bool created;

    void start()
    {
        created = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        StopWatch sw = GameObject.FindObjectOfType(typeof(StopWatch)) as StopWatch;
        if(sw.start && !created)
        {
            int max;
            if(AnswerStatus.level == 1)
            {
                max = 10;
            }
            else{
                max = 20;
            }
            int firstNum = Random.Range(max-9,max-1);
            int secondNum = Random.Range(1,max-firstNum);
            int ansNum = firstNum + secondNum;
            int[] balls = new int[]{firstNum, secondNum, ansNum};
            //Debug.Log(firstNum+" "+secondNum+" "+ansNum);
            for(int i=0;i<6;i++)
            {
                if(i<3)
                {
                    GameObject.Find("CircleHandler").GetComponent<CircleHandler>().SpawnNewCircle(balls[i]-1);
                }
                else{
                    GameObject.Find("CircleHandler").GetComponent<CircleHandler>().SpawnNewCircle(Random.Range(max/2,max)-1);
                }
            }
            created = true;
            MsgController mc = GameObject.FindObjectOfType(typeof(MsgController)) as MsgController;
            mc.displayLevel();
        }
    }
}
