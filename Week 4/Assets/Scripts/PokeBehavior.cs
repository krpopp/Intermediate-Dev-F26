using UnityEngine;

public class PokeBehavior : MonoBehaviour
{

    public enum Types
    {
        Dark,
        Poison
    }

    public Types myType;

    Transform target;

    Animator myAnimator;

    Vector2 dir;

    bool moving = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Cleffa").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f * Time.deltaTime);
        SetAnimation();
    }
    
    void SetAnimation()
    {
        Vector2 tempDir = (target.position - transform.position).normalized;
        if (tempDir != dir)
        {
            dir = tempDir;
            myAnimator.SetFloat("xVel", dir.x);
            myAnimator.SetFloat("yVel", dir.y);
        }
        if (moving)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }
}
