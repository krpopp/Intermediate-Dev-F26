using UnityEngine;
using UnityEngine.InputSystem;

public class BallMove : MonoBehaviour
{

    Rigidbody2D myBody;
    InputAction jump;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        jump = InputSystem.actions.FindAction("Jump");
        //myBody.AddForceY(500f);
        //myBody.AddForce(new Vector2(200f, 500f));
    }

    // Update is called once per frame
    void Update()
    {
        if (jump.IsPressed())
        {
            myBody.AddForceY(500f);
        }
    }
}
