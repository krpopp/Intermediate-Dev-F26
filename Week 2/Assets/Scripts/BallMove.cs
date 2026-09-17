using UnityEngine;
using UnityEngine.InputSystem;

public class BallMove : MonoBehaviour
{

    //reference to the ball's rigid body
    Rigidbody2D myBody;
    //reference to input we'll be listening for
    InputAction jump;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setting the rigidbody and input vars
        myBody = GetComponent<Rigidbody2D>();
        jump = InputSystem.actions.FindAction("Jump");
        //myBody.AddForceY(500f);
        //myBody.AddForce(new Vector2(200f, 500f));
    }

    // Update is called once per frame
    void Update()
    {
        //if any input assigned to the "jump" action is pressed
        if (jump.IsPressed())
        {
            //add an upwards force to the ball
            myBody.AddForceY(500f);
        }
    }
}
