using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallControl : MonoBehaviour
{

    InputAction launchButton; //reference to input system action
    Rigidbody2D myBody; //reference to the ball's rigidbody

    SpriteRenderer myRenderer; //reference to the ball's sprite renderer

    float pressTime; //how long the player presses the launch button
    bool launchBall = false; //if the ball should launch
    bool hasLaunched = false; //if the ball has launched

    Vector2 resetPosition; //starting position for reseting the ball

    public float pressMax; //the max the player can hold down the launch button
    public Color red, blue, green, yellow, purple; //colors the ball can be

    int score; //score

    public int colorPoints; //how many points hitting a color object adds

    public float launchMultiplier;
    public float bumperMultiplier;
    public float wallMultiplier;

    public TMP_Text scoreText;

    public GameObject startBlock;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set our input and component references
        launchButton = InputSystem.actions.FindAction("Launch");
        myBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        
        //find our starting position
        resetPosition = transform.position;

        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if we haven't launched
        if (!hasLaunched)
        {
            //if we press launch and haven't reached the max press time
            if (launchButton.IsPressed() && pressTime < pressMax)
            {
                //add the elapsed time since the last frame to pressTime
                pressTime += Time.deltaTime;
            }
            //if we release the launch button
            else if (launchButton.WasReleasedThisFrame())
            {
                //launch the ball
                launchBall = true;
            }
        }
    }
    
    
    //FixedUpdate is an event function that runs every physics set
    //this is slightly less often than Update BUT is steadier
    //anytime you interface with physics in code it should go in fixedupdate
    void FixedUpdate()
    {
        //if the ball hasn't launched yet
        if (launchBall)
        {
            //add upwards force to the ball
            //myBody.AddForceY(2000f * pressTime);
            myBody.linearVelocityY = launchMultiplier * pressTime;
            //set that we can try to launch again
            launchBall = false;
        }
    }

    //Fires when a 2D collision starts
    void OnCollisionEnter2D(Collision2D other)
    {
        //if the object we collided with is tagged a specific color
        //set the ball's color to that color
        //and add points to our score
        if (other.gameObject.CompareTag("Red"))
        {
            myRenderer.color = red;
            score += colorPoints;
            scoreText.text = score.ToString();
        } else if (other.gameObject.CompareTag("Blue"))
        {
            myRenderer.color = blue;
            score += colorPoints;
            scoreText.text = score.ToString();
        } else if (other.gameObject.CompareTag("Green"))
        {
            myRenderer.color = green;
            score += colorPoints;
            scoreText.text = score.ToString();
        } else if (other.gameObject.CompareTag("Yellow"))
        {
            myRenderer.color = yellow;
            score += colorPoints;
            scoreText.text = score.ToString();
        } else if (other.gameObject.CompareTag("Purple"))
        {
            myRenderer.color = purple;
            score += colorPoints;
            scoreText.text = score.ToString();
        }
        
        //if we hit a bumper
        //add an opposing force to the ball
        if (other.gameObject.CompareTag("Bumper"))
        {
            Vector2 contactPos = other.GetContact(0).normal;
            myBody.linearVelocity = contactPos * bumperMultiplier;
            //myBody.AddForce(-other.GetContact(0).normal * transform.up * 10000f, ForceMode2D.Force);
        }
        //if we hit the reset collider
        //reset our velocity, position, and gameplay bools
        if (other.gameObject.CompareTag("Reset"))
        {
            myBody.linearVelocity = Vector2.zero;
            transform.position = resetPosition;
            startBlock.SetActive(false);
            hasLaunched = false;
        }
        
        if(other.gameObject.CompareTag("Wall"))
        {
            Vector2 contactPos = other.GetContact(0).normal;
            myBody.linearVelocity = contactPos * wallMultiplier;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if we overlap with the object tagged Start and the blocking object isn't active
        if (other.gameObject.CompareTag("Start") && !startBlock.activeSelf)
        {
            //set that we've launched
            hasLaunched = true;
            //and turn on the blocking object
            startBlock.SetActive(true);
        }
    }
}
