using System;
using UnityEngine;

public class SubstituteBehavior : MonoBehaviour
{
    public bool inUse;
    public int index = -1;
    
    [SerializeField] float dieTime;
    [SerializeField] float dieTimeStep;
    
    void Update()
    {
        if (!inUse)
        {
            dieTime -= dieTimeStep * Time.deltaTime;
            if (dieTime <= 0)
            {
                GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSubs(index, true);
                GameObject.Find("Game Manager").GetComponent<GameManager>().subReady++;
                Destroy(gameObject);
                
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //if we hit an enemy and that enemy is targeting us
        //reset our substitute count
        //and destroy ourselves/the enemy
        if (other.CompareTag("Enemy") && other.GetComponent<PokeBehavior>().target == transform)
        {
            GameObject.Find("Canvas").GetComponent<UIManager>().UpdateSubs(index, true);
            GameObject.Find("Game Manager").GetComponent<GameManager>().subReady++;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
