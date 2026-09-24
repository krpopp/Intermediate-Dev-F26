using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperControl : MonoBehaviour
{

    //references to the flipper's body and input
    Rigidbody2D myBody;
    InputAction flipButton;

    //reference to the specific flipper's action name (eg: left or right)
    public string actionName;

    public float flipMultiplier;
    float flipTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the references declared above start
        myBody = GetComponent<Rigidbody2D>();
        flipButton = InputSystem.actions.FindAction(actionName);
    }

    void FixedUpdate()
    {
        //if we press this object's input
        if (flipButton.IsPressed())
        {
            //add upwards force to the flipper
            myBody.AddForceY(200f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballBody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 contactPos = -other.GetContact(0).normal;
            ballBody.linearVelocity = contactPos * flipMultiplier;
        }
    }
}
