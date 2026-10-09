using System;
using UnityEngine;

public class CleffaBehavior : MonoBehaviour
{

    //initial stats
    public float fullnessVal = 10f; 
    public float sleepinessVal = 10f;
    public float hpVal = 10f;
    public float xpVal = 0f;

    //timer for incrementing needs
    float needsTime;
    [SerializeField] float needsTimeReset; 
    [SerializeField] float needsTimeStep;

    //reference to the game manager script in the scene
    public GameManager myManager; 

    //where we move to and our direction
    Vector3 targetPos; 
    Vector2 dir;
    
    Animator myAnimator;

    enum States
    {
        Idle,
        Moving,
        Sleeping,
        Eating
    }

    States state = States.Idle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the needs timer
        needsTime = needsTimeReset; 
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //decrement the needs timer
        needsTime -= needsTimeStep * Time.deltaTime;
        //if the timer reached 0
        if (needsTime < 0) 
        {
            //update our cleffa's needs
            IncrementNeeds();
            CheckNeeds();
        }
        RunState();
        GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSliders();
    }

    void RunState()
    {
        switch (state)
        {
            case States.Idle:
                break;
            case States.Moving:
                //set our position to the next step towards our target
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.5f * Time.deltaTime);
                //if we reached the target, stop moving
                if (Vector3.Distance(transform.position, targetPos) < 0.05f) ChangeState(States.Idle);
                break;
            case States.Sleeping:
                break;
        }
    }

    void ChangeState(States newState)
    {
        switch (newState)
        {
            case States.Idle:
                myAnimator.SetBool("isSleeping", false);
                myAnimator.SetBool("isWalking", false);
                dir = new Vector2(0,-1);
                break;
            case States.Moving:
                myAnimator.SetBool("isSleeping", false);
                myAnimator.SetBool("isWalking", true);
                break;
            case States.Sleeping:
                myAnimator.SetBool("isSleeping", true);
                myAnimator.SetBool("isWalking", false);
                break;
        }
        state = newState;
        SetAnimationDirection();
    }

    //function to handle reduce cleffa's needs
    void IncrementNeeds()
    {

        switch (state)
        {
            case States.Idle:
            case States.Moving:
                if (fullnessVal > 0) fullnessVal -= 1;
                else if(fullnessVal <= 0) hpVal -= 1;
                if (sleepinessVal > 0) sleepinessVal -= 1;
                break;
            case States.Sleeping:
                if(sleepinessVal < 10) sleepinessVal += 1;
                break;
        }
        
        //if our hp is less than 10, but our other stats are over half
        if (hpVal < 10 && sleepinessVal > 5 && fullnessVal > 5)
        {
            //increase our hp stat
            hpVal += 1;
        }
        
        needsTime = needsTimeReset; 
    }

    void CheckNeeds()
    {
        switch (state)
        {
            case States.Idle:
                if(sleepinessVal <= 0) ChangeState(States.Sleeping);
                else if(fullnessVal <= 5) FindFood();
                break;
            case States.Moving:
                break;
            case States.Sleeping:
                if(sleepinessVal == 0) ChangeState(States.Idle);
                break;
        }
    }

    //function to find the nearest food in the scene
    void FindFood()
    {
        //setting the initial distance we're checking against
        float dist = 2000f;
        //tracks the nearest food
        GameObject closestFood = null; 
        //loop through the game manager's food list
        foreach (GameObject food in myManager.allFood)
        {
            //if this food is closer than the last food we checked
            //(or if the food is closet than the initial value we set dist to
            if (Vector3.Distance(transform.position, food.transform.position) < dist)
            {
                //set dist to the new closest distance
                dist = Vector3.Distance(transform.position, food.transform.position);
                //set the var tracking the closest food to this food
                closestFood = food;
            }
        }
        //if we found a food
        if (closestFood != null)
        {
            //set the target position to that food's position
            targetPos = closestFood.transform.position;
            ChangeState(States.Moving);
        }
    }

    //change our animation based on our current state and direction
    void SetAnimationDirection()
    {
        Vector2 tempDir = (targetPos - transform.position).normalized;
        if (tempDir != dir)
        {
            dir = tempDir;
            myAnimator.SetFloat("xVel", dir.x);
            myAnimator.SetFloat("yVel", dir.y);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //based on what we hit and its typing, adjust our hp and xp
        if (other.gameObject.TryGetComponent<PokeBehavior>(out PokeBehavior otherPoke))
        {
            if (otherPoke.type == PokeBehavior.Type.Dark && otherPoke.target == transform)
            {
                xpVal += 1;
                Destroy(other.gameObject);
            } else if (otherPoke.type == PokeBehavior.Type.Poison && otherPoke.target == transform)
            {
                xpVal += 2;
                hpVal -= 1;
                Destroy(other.gameObject);
            }
        }

        //if we hit the food, eat the food
        if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.GetComponent<FoodBehavior>().RemoveFood();
            fullnessVal += 2;
        }
    }
}
