using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        int firstNum = Random.Range(1,10);
        int secondNum = Random.Range(1,20-firstNum);
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
                GameObject.Find("CircleHandler").GetComponent<CircleHandler>().SpawnNewCircle(Random.Range(0,20));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
