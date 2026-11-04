using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //declare reference to WASD inputs
    private InputAction moveInput;

    //amount of force we want to add to the player
    //public float force;
    
    //speed and power for movement and jumping
    public float speed;
    public float jumpPower;
    
    //reference to the attached gameobject's rigidbody and collider
    private Rigidbody2D myBody;
    private BoxCollider2D myCollider;
    //to track the vector returned by moveInput
    private Vector2 inputForce;
    
    //length we check if we touched the ground
    public float checkLength;
    //layer that the ground is in 
    public LayerMask groundLayer;
    
    //tracks if we're on the ground layer or not
    private bool onGround;

    [SerializeField] private ParticleSystem myDust;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
        //set the move inputs
        moveInput = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        inputForce = moveInput.ReadValue<Vector2>();
    }

    //event function where we put physics stuff
    //runs at the same rate as the physics engine
    private void FixedUpdate()
    {
        Vector3 newVelocity = myBody.linearVelocity;
        
        //set the x velocity to the horizontal input direction, multiplied by the speed
        newVelocity.x = inputForce.x * speed;
        
        //if we pressed the up input and the player is on the ground
        if (inputForce.y > 0 && onGround && myBody.linearVelocity.y == 0) 
        {
            //set the y velocity to the vertical input direction, multiplied by the jump power
            newVelocity.y = inputForce.y * jumpPower;
            //set that we are no longer on the ground
            onGround = false;
        }
        else
        {
            Vector3 startRay = myCollider.bounds.center;
            float dist = myCollider.bounds.extents.y + checkLength;
            onGround = Physics2D.Raycast(startRay, Vector3.down, dist, groundLayer);
        }
        
        myBody.linearVelocity = newVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (myBody.linearVelocityY <= 0 && other.gameObject.tag == "Ground")
        {
            CameraShake.ShakeScreen(20f);
            myDust.Play();
        }
    }
}