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
    
    //CHANGE: tracking which state we're in
    bool moving; 
    bool sleeping;
    
    Animator myAnimator;
    
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
        }

        //if we're moving
        if (moving) 
        {
            //set our position to the next step towards our target
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.5f * Time.deltaTime);
            //if we reached the target, stop moving
            if (Vector3.Distance(transform.position, targetPos) < 0.05f) moving = false;
        }
        //SetAnimation();
    }

    //function to handle reduce cleffa's needs
    void IncrementNeeds()
    {
        //if we're not at 0, decrease the fullness value
        if(fullnessVal > 0) fullnessVal -= 1; 
        //if sleepiness is 0 or less
        if (sleepinessVal <= 0) {
            //start sleeping
            sleeping = true;
            //but if our fullness is low, we're not moving and we're not sleeping
        } else if (fullnessVal <= 5 && !moving && !sleeping) //if the fullness value has reached 0
        {
            //find the nearest food object
            FindFood();
        }

        //if we're sleeping
        if (sleeping)
        {
            //improve our sleep stat
            sleepinessVal += 1;
            //if we've maxed out our sleep stat, stop sleeping
            if (sleepinessVal == 10) sleeping = false;
        } else
        {
            //otherwise, become sleepier
            sleepinessVal -= 1;
        }
        //if our hp is less than 10, but our other stats are over half
        if (hpVal < 10 && sleepinessVal > 5 && fullnessVal > 5)
        {
            //increase our hp stat
            hpVal += 1;
        }
        //update UI and animation based on the previous steps
        GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSliders();
        SetAnimation();
        needsTime = needsTimeReset; 
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
            //set moving to true
            moving = true;
        }
        else
        {
            //if we didn't find a food and our fullness is less than 0, decrease our hp
            if (fullnessVal <= 0)
            {
                hpVal -= 1;
            }
        }
    }

    //change our animation based on our current state and direction
    void SetAnimation()
    {
        Vector2 tempDir = (targetPos - transform.position).normalized;
        if (tempDir != dir && moving)
        {
            dir = tempDir;
            myAnimator.SetFloat("xVel", dir.x);
            myAnimator.SetFloat("yVel", dir.y);
        }

        if (sleeping)
        {
            myAnimator.SetBool("isSleeping", true);
        }
        else if (moving)
        {
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isSleeping", false);
            myAnimator.SetBool("isWalking", false);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //CHANGE: based on what we hit and its typing, adjust our hp and xp
        if (other.gameObject.TryGetComponent<PokeBehavior>(out PokeBehavior otherPoke))
        {
            if (otherPoke.myType == 0 && otherPoke.target == transform)
            {
                xpVal += 1;
                Destroy(other.gameObject);
            } else if (otherPoke.myType == 1 && otherPoke.target == transform)
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
            GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSliders();
        }
    }
}
