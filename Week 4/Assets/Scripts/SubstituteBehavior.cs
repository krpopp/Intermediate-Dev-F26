using System;
using UnityEngine;

public class SubstituteBehavior : MonoBehaviour
{
    public bool inUse;
    public int index = -1;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<PokeBehavior>().target == transform)
        {
            UIManager.UpdateSubs(index, true);
            GameManager.subReady++;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
