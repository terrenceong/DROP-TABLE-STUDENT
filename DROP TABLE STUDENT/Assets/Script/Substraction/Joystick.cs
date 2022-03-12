using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform Player;
    public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 oldPosition;
    private Vector2 oldPositionBG;
    private Rigidbody2D myBody;
    private Animator myanim;
    private SpriteRenderer mysr;
    private string WALK_ANIMATION = "walking";
    [SerializeField]
    private Transform circle;
    [SerializeField]
    private Transform outerCircle;



    // Start is called before the first frame update
    void Start()
    {
        outerCircle = GameObject.FindGameObjectWithTag("JoystickBG").GetComponent<Transform>();
        circle = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Transform>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        myBody = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
        mysr = GetComponent<SpriteRenderer>();
        oldPosition = circle.transform.position;
        oldPositionBG = outerCircle.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //movePlayer(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))); /* keyboard movement
        if(Input.GetMouseButtonDown(0))
        {

            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            //circle.transform.position = pointA;
            //outerCircle.transform.position = pointA;
            circle.transform.position = oldPosition;
            outerCircle.transform.position = oldPositionBG;
          

            //circle.GetComponent<SpriteRenderer>().enabled = true;
            //outerCircle.GetComponent<SpriteRenderer>().enabled = true;

        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            myanim.SetBool(WALK_ANIMATION, true);
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
            myanim.SetBool(WALK_ANIMATION, false);
            circle.transform.position = oldPosition;
            outerCircle.transform.position = oldPositionBG;
        }
           
    }
    void movePlayer(Vector2 direction)
    {
        Player.Translate(direction * speed * Time.deltaTime);
        
    }
    private void FixedUpdate()
    {
        if(touchStart)
        {
           
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            movePlayer(direction);
            if (direction.x > 0)
            {
                mysr.flipX = false;
            }
            else if (direction.x < 0)
            {
                mysr.flipX = true;
            }
            circle.transform.position = new Vector2(oldPosition.x+ direction.x, oldPosition.y + direction.y);


        }
     
    }
}
