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

    //list of transforms where other pokemon can spawn
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    //list of other pokemon we can spawn
    [SerializeField] List<GameObject> otherPoke = new List<GameObject>();

    //timer for making pokemon
    float spawnTime;
    [SerializeField] float spawnTimeReset;
    [SerializeField] float spawnTimeStep;

    //number of substitute objects we can make
    public int subReady = 3;
    
    [SerializeField] GameObject subObj;
    
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
            CheckHover();
        }
        //count down to making another enemy
        spawnTime -= spawnTimeReset * Time.deltaTime;
        if (spawnTime <= 0)
        {
            MakeEnemy();
        }
    }

    //create a food near the tree we clicked on
    void MakeFood()
    {
        //get the mouse's position, translated to the world's space
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.x += 0.2f;
        newPos.z = 0; //set the z to 0 so the food is in the camera's view
        //create a new food object and add it to the food list
        allFood.Add(Instantiate(foodObj, newPos, Quaternion.identity)); 
    }

    //create a new enemy at a random spot and restart the enemy timer
    void MakeEnemy()
    {
        int randSpot = Random.Range(0, spawnPoints.Count);
        int randPoke = Random.Range(0, otherPoke.Count);
        Instantiate(otherPoke[randPoke], spawnPoints[randSpot].position, Quaternion.identity);
        spawnTime = spawnTimeReset;
    }

    //create a substitute object to distract an enemy
    void MakeSub()
    {
        //get the mouse's position, translated to the world's space
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //set the z to 0 so the substitute is in the camera's view
        newPos.z = 0; 
        //make the substitute
        GameObject newSub = Instantiate(subObj, newPos, Quaternion.identity);
        newSub.transform.name = "Substitute";
        //mark that we've used up a substitute and reset the game accordingly
        subReady--;
        newSub.GetComponent<SubstituteBehavior>().index = subReady;
        GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSubs(subReady, false);
    }
    
    void CheckHover()
    {
        //find what we're hovering over
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        //if we've hovering on something
        if (hit != null)
        {
            //and if that thing is a tree
            if (hit.transform.gameObject.TryGetComponent<TreeBehavior>(out TreeBehavior treeScript))
            {
                //and that tree has a berry 
                if (treeScript.berryReady)
                {
                    //make a berry, reset the tree's growth
                    MakeFood();
                    treeScript.ResetTree();
                }
            } 
        }
        else
        {
            if(subReady > 0) MakeSub();
        }
    }
}
