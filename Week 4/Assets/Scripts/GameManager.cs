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

    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] List<GameObject> otherPoke = new List<GameObject>();

    float spawnTime;
    [SerializeField] float spawnTimeReset;
    [SerializeField] float spawnTimeStep;

    public static int subReady = 3;
    [SerializeField] GameObject subObj;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the leftMouse action
        leftMouse = InputSystem.actions.FindAction("MouseClick");
        MakeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        //if left mouse was released this frame
        if (leftMouse.WasReleasedThisFrame())
        {
            CheckHover();
            //create a food
            //MakeFood();
        }

        spawnTime -= spawnTimeReset * Time.deltaTime;
        if (spawnTime <= 0)
        {
            MakeEnemy();
        }
        Debug.Log(subReady);
    }

    void MakeFood()
    {
        //get the mouse's position, translated to the world's space
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.x += 0.2f;
        newPos.z = 0; //set the z to 0 so the food is in the camera's view
        //create a new food object and add it to the food list
        allFood.Add(Instantiate(foodObj, newPos, Quaternion.identity)); 
    }

    void MakeEnemy()
    {
        int randSpot = Random.Range(0, spawnPoints.Count);
        int randPoke = Random.Range(0, otherPoke.Count);
        Instantiate(otherPoke[randPoke], spawnPoints[randSpot].position, Quaternion.identity);
        spawnTime = spawnTimeReset;
    }

    void MakeSub()
    {
        //get the mouse's position, translated to the world's space
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.z = 0; //set the z to 0 so the food is in the camera's view
        GameObject newSub = Instantiate(subObj, newPos, Quaternion.identity);
        newSub.transform.name = "Substitute";
        subReady--;
        newSub.GetComponent<SubstituteBehavior>().index = subReady;
        UIManager.UpdateSubs(subReady, false);
        Debug.Log("make");
    }
    
    void CheckHover()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null)
        {
            if (hit.transform.gameObject.TryGetComponent<TreeBehavior>(out TreeBehavior treeScript))
            {
                if (treeScript.berryReady)
                {
                    MakeFood();
                    treeScript.ResetTree();
                }
            } else if (hit.CompareTag("PlayArea"))
            {
                Debug.Log("clicked");
                if(subReady > 0) MakeSub();
            }
        }
    }
}
