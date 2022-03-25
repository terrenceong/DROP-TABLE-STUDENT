using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject[] newCircle;

    public void SpawnNewCircle(int index)
    {
        GameObject nb = Instantiate(newCircle[index], this.transform) as GameObject;
        nb.name = nb.name.Replace("(Clone)","");
        nb.transform.localPosition = new Vector3(Random.Range(-4f,6f),0.1f,0);
    }
}
