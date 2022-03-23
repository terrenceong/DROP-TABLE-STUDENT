using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject[] newCircle;

    public void SpawnNewCircle()
    {
        GameObject nb = Instantiate(newCircle[Random.Range(0,newCircle.Length)], this.transform) as GameObject;
    }
}
