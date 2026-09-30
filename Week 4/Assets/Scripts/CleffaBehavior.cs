using UnityEngine;

public class CleffaBehavior : MonoBehaviour
{
    
    float fullnessVal = 5f; //stat tracking how hungry cleffa is
    
    float needsTime; //timer
    public float needsTimeReset; //what we reset the timer to when it goes off
    public float needsTimeStep; //speed the timer goes down

    public GameManager myManager; //reference to the game manager script in the scene

    Vector3 targetPos; //position we go to when we move
    bool moving; //tracks if we're moving or not
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        needsTime = needsTimeReset; //set the needs timer
    }

    // Update is called once per frame
    void Update()
    {
        needsTime -= needsTimeStep * Time.deltaTime; //decrement the needs timer
        if (needsTime < 0) //if the timer reached 0
        {
            IncrementNeeds(); //decrement our cleffa's needs
        }

        if (moving) //if we're moving
        {
            //set our position to the next step towards our target
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 2f * Time.deltaTime);
        }
    }

    //function to handle reduce cleffa's needs
    void IncrementNeeds()
    {
        fullnessVal -= 1; //decrease the fullness stat
        needsTime = needsTimeReset; //reset the needs timer
        Debug.Log(fullnessVal); //check our fullness value in the console
        if (fullnessVal <= 0) //if the fullness value has reached 0
        {
            FindFood(); //find the nearest food object
        }
    }

    void FindFood()
    {
        float dist = 2000f; //setting the initial distance we're checking against
        GameObject closestFood = null; //tracks the nearest food
        //loop through the game manager's food list
        foreach(GameObject food in myManager.allFood)
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
    }
}
