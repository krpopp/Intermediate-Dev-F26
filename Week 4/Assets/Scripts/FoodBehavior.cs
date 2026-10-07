using UnityEngine;

public class FoodBehavior : MonoBehaviour
{

    //how long a berry lives
    [SerializeField] float lifeTime;
    [SerializeField] float lifeTimeStep;
    
    // Update is called once per frame
    void Update()
    {
        //count down to our death
        lifeTime -= lifeTimeStep * Time.deltaTime;
        if (lifeTime <= 0)
        {
            RemoveFood();
        }
    }

    //remove the food from the food list and destroy myself
    public void RemoveFood()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().allFood.Remove(gameObject);
        Destroy(gameObject);
    }
}
