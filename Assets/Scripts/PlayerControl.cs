using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    string playerName = "Karina";
    int playerScore = 10;
    float playerHealth = 2.4f;
    bool playerDead = false;
    char playerID = 'f';

    public float playerSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            newPos.y = newPos.y + playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPos.y = newPos.y - playerSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            newPos.x = newPos.x - playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPos.x = newPos.x + playerSpeed * Time.deltaTime;
        }
        transform.position = newPos;
    }
}
