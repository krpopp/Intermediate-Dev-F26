using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    //input we set up in our Input System asset for mouse clicks
    InputAction leftMouse;

    //prefab for food we want to make on click
    public GameObject foodObj;

    //list tracking the food that we've made
    //need to inlude System.Collections.Generic; in the namespaces up top
    public List<GameObject> allFood = new List<GameObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the leftMouse action
        leftMouse = InputSystem.actions.FindAction("MouseClick");
    }

    // Update is called once per frame
    void Update()
    {
        //if left mouse was released this frame
        if (leftMouse.WasReleasedThisFrame())
        {
            //create a food
            MakeFood();
        }
    }

    void MakeFood()
    {
        //get the mouse's position, translated to the world's space
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.z = 0; //set the z to 0 so the food is in the camera's view
        //create a new food object and add it to the food list
        allFood.Add(Instantiate(foodObj, newPos, Quaternion.identity)); 
    }
}
