using System;
using UnityEngine;

public class FishControl : MonoBehaviour
{

    Animator fishAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishAnim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fishAnim.SetTrigger("doSwim");
        }
    }
}
