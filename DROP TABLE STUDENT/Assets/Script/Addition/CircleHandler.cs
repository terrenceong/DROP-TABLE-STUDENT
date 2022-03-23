using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject[] newCircle;

    public void SpawnNewCircle()
    {
        GameObject nb = Instantiate(newCircle[Random.Range(0,newCircle.Length)], this.transform) as GameObject;
        nb.name = nb.name.Replace("(Clone)","");
        nb.transform.localPosition = new Vector3(Random.Range(-2f,2f),0.08f,0);
    }
}
