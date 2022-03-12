using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjScript : MonoBehaviour
{
    [SerializeField]
    private Text collectedText;
    private string OBJECTIVE1_TAG = "Objective1";
    private string OBJECTIVE2_TAG = "Objective2";
    private string PLAYER_TAG = "Player";
    GameObject gameSpawner;
    ObjSpawner gameObjSpawner;

    private Rigidbody2D theObject;
    // Start is called before the first frame update
    void Start()
    {
        gameSpawner=GameObject.Find("Spawner");
        collectedText = GameObject.FindGameObjectWithTag("Result").GetComponent<Text>();
        gameObjSpawner = gameSpawner.GetComponent<ObjSpawner>();
        theObject = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag(OBJECTIVE1_TAG) && gameObject.CompareTag(PLAYER_TAG)){
            if(gameObjSpawner.checker==false && gameObjSpawner.spawnedObjects <= 6){
                gameObjSpawner.checker=true;
            }
            Destroy(collision.gameObject);
            gameObjSpawner.spawnedObjects--;
            collectedText.text=""+(int.Parse(collectedText.text)+1);
        }
        else if(collision.gameObject.CompareTag(OBJECTIVE2_TAG) && gameObject.CompareTag(PLAYER_TAG)){
            if(gameObjSpawner.checker==false && gameObjSpawner.spawnedObjects <= 6){
                gameObjSpawner.checker=true;
            }
            Destroy(collision.gameObject);
            gameObjSpawner.spawnedObjects--;
            collectedText.text=""+(int.Parse(collectedText.text)+3);
        }
    }
}
