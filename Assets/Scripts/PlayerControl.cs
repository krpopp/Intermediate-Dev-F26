using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    string playerName = "Karina";
    int playerScore = 10;
    float playerHealth = 2.4f;
    bool playerDead = false;
    char playerID = 'f';

    public float playerAcceleration;
    Vector3 velocity = Vector3.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity *= 0.98f;
        Vector3 newPos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y += playerAcceleration;
            //newPos.y = newPos.y + playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= playerAcceleration;
            //newPos.y = newPos.y - playerSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A) && newPos.x > 1)
        {
            velocity.x -= playerAcceleration;
            //newPos.x = newPos.x - playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) && newPos.x < 10)
        {
            velocity.x += playerAcceleration;
            //newPos.x = newPos.x + playerSpeed * Time.deltaTime;
        }

        newPos += velocity * Time.deltaTime;
        transform.position = newPos;
    }
}
