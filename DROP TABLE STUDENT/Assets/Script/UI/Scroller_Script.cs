using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller_Script : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1 * Time.deltaTime, 0);

        if (transform.position.x < -19)
        {
            transform.position = new Vector3(18.26f, transform.position.y, transform.position.z);
        }
    }
}
