using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjSpawner : MonoBehaviour
{
    private Transform Player;
    private Transform Portal;
    public SpriteRenderer[] objectiveReference;

    private Vector3 newObj;
    private SpriteRenderer spawnedObjective;
    private Collider objectCollider;

    
    //[SerialField]
    public Transform centrePos;
    private int randomIndex;
    public int spawnedObjects = 0;
    public bool checker;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Portal = GameObject.FindGameObjectWithTag("Portal").GetComponent<Transform>();
        if (spawnedObjects < 6){
            StartCoroutine(SpawnObjective());
        }
    }
    void Update(){
        if(spawnedObjects < 6 && checker==true){
            StartCoroutine(SpawnObjectiveOnce());
        }
    }

    IEnumerator SpawnObjective(){
        while(spawnedObjects < 6){
            yield return new WaitForSeconds(Random.Range(1,1));
            createObjective();
        }
        checker=true;
    }

    IEnumerator SpawnObjectiveOnce(){
        checker=false;
        yield return new WaitForSeconds(Random.Range(2,8));
        createObjective();
    }
    
    private void createObjective(){
        randomIndex = Random.Range(0, objectiveReference.Length);
        
        spawnedObjective = Instantiate(objectiveReference[randomIndex]);
        spawnedObjects++;
        do{
            newObj = new Vector3(Random.Range(-6.5f,5.5f), Random.Range(-3.8f,2.0f), 0);
        }
        while(getRelativePosition(newObj));
        spawnedObjective.transform.position = centrePos.position + newObj;

        objectCollider = spawnedObjective.GetComponent<Collider>();
    }

    private bool getRelativePosition(Vector3 spawnPosition) {
        Vector3 playerPosition = Player.transform.position;
        Vector3 portalPosition = Player.transform.position;
        Vector3 playerDistance = playerPosition - spawnPosition;
        Vector3 portalDistance = portalPosition - spawnPosition;
        //UnityEngine.Debug.Log(position+"player");
        if(Mathf.Abs(playerDistance.x) < 2 || Mathf.Abs(playerDistance.y) < 1){
            return true;
        }
        //UnityEngine.Debug.Log(spawnedObjects+"success");
        return false;
    }
}
