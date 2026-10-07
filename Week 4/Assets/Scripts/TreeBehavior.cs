using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    
    float growthTime; //timer
    [SerializeField] float growthTimeReset; //what we reset the timer to when it goes off
    [SerializeField] float growthTimeStep; //speed the timer goes down

    //phase the tree is in
    int growSize = 0;

    //can the tree make a berry?
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
        //if there isn't a berry ready to pick
        if (!berryReady)
        {
            growthTime -= growthTimeStep * Time.deltaTime; //decrement the growth timer
            if (growthTime <= 0)
            {
                //grow the tree
                Grow();
            }
        }
    }

    //grow the tree, set if a berry can be picked
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

    //after a berry is picked, reset all the values of the tree so it can grow again
    public void ResetTree()
    {
        growthTime = growthTimeReset;
        growSize = 0;
        myAnimator.SetInteger("level", growSize);
        berryReady = false;
    }
}
