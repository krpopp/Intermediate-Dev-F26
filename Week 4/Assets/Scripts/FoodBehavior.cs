using UnityEngine;

public class FoodBehavior : MonoBehaviour
{

    [SerializeField] float lifeTime;
    [SerializeField] float lifeTimeStep;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= lifeTimeStep * Time.deltaTime;
        if (lifeTime <= 0)
        {
            RemoveFood();
        }
    }

    public void RemoveFood()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().allFood.Remove(gameObject);
        Destroy(gameObject);
    }
}
