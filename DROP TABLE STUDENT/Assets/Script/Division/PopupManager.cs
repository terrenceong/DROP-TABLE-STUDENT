using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    protected virtual void Start()
    {
    }

    public void pop()
    {
        gameObject.GetComponent<Animator>().SetTrigger("pop");
        Debug.Log("PopupManager: Pop popup.");
    }

    public void close(){
        gameObject.GetComponent<Animator>().SetTrigger("close");
        Debug.Log("PopupManager: Close popup.");
    }
}
