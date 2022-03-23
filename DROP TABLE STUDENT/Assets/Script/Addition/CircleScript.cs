using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<6;i++)
        {
            GameObject.Find("CircleHandler").GetComponent<CircleHandler>().SpawnNewCircle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
