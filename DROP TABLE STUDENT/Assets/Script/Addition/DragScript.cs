using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DragScript : MonoBehaviour
{
    //touch offset allows ball not to shake when it starts to move
    float deltaX, deltaY;

    //reference to Rigidbody2D component
    Rigidbody2D rb;

    //ball movement not allowed if you haven't touch the ball
    bool moveAllowed = false;

    //Status of ball
    String placed = "Ground";

    [SerializeField] private Text GameEnd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Add bouncy material to the ball
        PhysicsMaterial2D mat = new PhysicsMaterial2D();
        mat.bounciness = 0.5f;
        mat.friction = 0.04f;
        GetComponent<CircleCollider2D>(). sharedMaterial = mat;
        GameEnd.GetComponent<Text>().enabled = false;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Answer")
        {   
            Debug.Log(collision.otherRigidbody.name + " has been placed on " + collision.gameObject.name);
            //Debug.Log(Int32.Parse(collision.otherRigidbody.name)); //convert String to int
            if(collision.gameObject.name == "Ans-1")
            {
                if(placed != "Ground"){
                    if(placed == "Ans-2")
                    {
                        AnswerStatus.setAns2(0);
                        Debug.Log("Ans-2 has been set to 0");
                    }
                    else if(placed == "Ans-3")
                    {
                        AnswerStatus.setAns3(0);
                        Debug.Log("Ans-3 has been set to 0");
                    }
                }
                AnswerStatus.setAns1(Int32.Parse(collision.otherRigidbody.name));
                Debug.Log(AnswerStatus.getAns1().ToString() + " has been set in Ans-1");
                placed = "Ans-1";
            }
            else if(collision.gameObject.name == "Ans-2")
            {
                if(placed != "Ground" || placed != "Ans-2"){
                    if(placed == "Ans-1")
                    {
                        AnswerStatus.setAns1(0);
                        Debug.Log("Ans-1 has been set to 0");
                    }
                    else if(placed == "Ans-3")
                    {
                        AnswerStatus.setAns3(0);
                        Debug.Log("Ans-3 has been set to 0");
                    }
                }
                AnswerStatus.setAns2(Int32.Parse(collision.otherRigidbody.name));
                Debug.Log(AnswerStatus.getAns2().ToString() + " has been set in Ans-2");
                placed = "Ans-2";
            }
            else if(collision.gameObject.name == "Ans-3")
            {
                if(placed != "Ground" || placed != "Ans-3"){
                    if(placed == "Ans-1")
                    {
                        AnswerStatus.setAns1(0);
                        Debug.Log("Ans-1 has been set to 0");
                    }
                    else if(placed == "Ans-2")
                    {
                        AnswerStatus.setAns2(0);
                        Debug.Log("Ans-2 has been set to 0");
                    }
                }
                AnswerStatus.setAns3(Int32.Parse(collision.otherRigidbody.name));
                Debug.Log(AnswerStatus.getAns3().ToString() + " has been set in Ans-3");
                placed = "Ans-3";
            }
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.otherRigidbody.name + " has been placed on the ground");
            if(placed != "Ground")
            {
                if(placed == "Ans-1")
                {
                   AnswerStatus.setAns1(0);
                   Debug.Log(AnswerStatus.getAns1().ToString() + " has been set in Ans-1");
                }
                else if(placed == "Ans-2")
                {
                    AnswerStatus.setAns2(0); 
                    Debug.Log(AnswerStatus.getAns2().ToString() + " has been set in Ans-2");
                }
                else //Ans-3
                {
                    AnswerStatus.setAns3(0); 
                    Debug.Log(AnswerStatus.getAns3().ToString() + " has been set in Ans-3");
                }
            }
        }
        if(AnswerStatus.allAnswered())
        {
            Debug.Log(AnswerStatus.getAns1() + "+" + AnswerStatus.getAns2() + "=" + AnswerStatus.getAns3());
            if(AnswerStatus.getAns1() + + + AnswerStatus.getAns2() == AnswerStatus.getAns3())
            {
                //Debug.Log("WELL DONE!");
                //FindObjectOfType<GameManager>().DisplayOnScreen("Well Done!");
                GameEnd.GetComponent<Text>().text = "WELL DONE!";
                GameEnd.GetComponent<Text>().fontSize = 250;
                GameEnd.GetComponent<Text>().color = Color.green;
                GameEnd.GetComponent<Text>().enabled = true;

            }
            else
            {
                //Debug.Log("FAIL!");
                //FindObjectOfType<GameManager>().DisplayOnScreen("FAIL!");
                GameEnd.GetComponent<Text>().text = "Try Again!";
                GameEnd.GetComponent<Text>().fontSize = 250;
                GameEnd.GetComponent<Text>().color = Color.red;
                GameEnd.GetComponent<Text>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Initiating touch event
        //if touch event takes place
        if(Input.touchCount > 0){
            //get touch
            Touch touch = Input.GetTouch(0);
            //obtain touch position
            Vector2 TouchPos = Camera.main.ScreenToWorldPoint(touch.position);
            //processing touch phases
            switch(touch.phase){
                //if you touch the ball
                case TouchPhase.Began:
                    //if you touch the ball
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(TouchPos)){
                        //get the offset between position you touches
                        //and the center of the game object
                        deltaX = TouchPos.x - transform.position.x;
                        deltaY = TouchPos.y - transform.position.y;

                        //if touch begins within the ball collider
                        //then it is allowed to move
                        moveAllowed = true;

                        //restrict some regidbody properties so it moves more smoothly and correctly
                        rb.freezeRotation = true;
                        rb.velocity = new Vector2(0,0);
                        rb.gravityScale = 0;
                        GetComponent<CircleCollider2D>().sharedMaterial = null;
                    }
                    break;

                    //you move your finger
                case TouchPhase.Moved:
                //if you touch the ball and movement is allowed
                    if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(TouchPos) && moveAllowed)
                    {
                        rb.MovePosition(new Vector2 (TouchPos.x - deltaX, TouchPos.y - deltaY));
                    }
                    break;
                
                case TouchPhase.Ended:
                // restore initial parameters when touch ended
                    moveAllowed = false;
                    rb.freezeRotation = false;
                    rb.gravityScale = 2;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D();
                    mat.bounciness=0.5f;
                    mat.friction = 0.04f;
                    GetComponent<CircleCollider2D>().sharedMaterial = mat;
                break;
            }

        }
    }
}
