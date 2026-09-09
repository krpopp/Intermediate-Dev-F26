using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //variables used by this whole script are declared here
    
    //examples of variables and data types
    //we're not actually using any of these
    string playerName = "Karina";
    int playerScore = 10;
    float playerHealth = 2.4f;
    bool playerDead = false;
    char playerID = 'f';

    //how quickly my player should speed up
    public float playerAcceleration;
    //current velocity of my player
    Vector3 velocity = Vector3.zero;
    

    // Update is called once per frame
    void Update()
    {
        //slow down every frame
        velocity *= 0.98f;
        
        //get my player's current position
        //(I'm using vector 3 for teaching purposes; you can use vector2 if you want)
        Vector3 newPos = transform.position;
        
        //if I press the w or s keys, increase my y velocity correspondingly
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y += playerAcceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= playerAcceleration;
        }
        
        //if I press the a or d keys AND i'm within a specific area of the world
        //increase my x velocity correspondingly
        if(Input.GetKey(KeyCode.A) && newPos.x > 1)
        {
            velocity.x -= playerAcceleration;
        }
        if (Input.GetKey(KeyCode.D) && newPos.x < 10)
        {
            velocity.x += playerAcceleration;
        }

        //add my velocity to my position
        //and multiply it by delta time to make frame independent
        //this makes sure my player moves at the same speed no matter the FPS of my game
        newPos += velocity * Time.deltaTime;
        
        //set the player's position to my new position
        transform.position = newPos;
    }
}
