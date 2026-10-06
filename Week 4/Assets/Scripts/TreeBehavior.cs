using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    
    float growthTime; //timer
    public float growthTimeReset; //what we reset the timer to when it goes off
    public float growthTimeStep; //speed the timer goes down

    int growSize = 0;

    public bool berryReady;

    Animator myAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        growthTime = growthTimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!berryReady)
        {
            growthTime -= growthTimeStep * Time.deltaTime; //decrement the growth timer
            if (growthTime <= 0)
            {
                Grow();
            }
        }
    }

    void Grow()
    {
        growSize++;
        if (growSize == 4)
        {
            berryReady = true;
        }
        myAnimator.SetInteger("level", growSize);
        growthTime = growthTimeReset;
    }

    public void ResetTree()
    {
        growthTime = growthTimeReset;
        growSize = 0;
        myAnimator.SetInteger("level", growSize);
        berryReady = false;
    }
}
