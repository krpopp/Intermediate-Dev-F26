using System;
using UnityEngine;

public class FrogControl : MonoBehaviour
{

    public Animator frogAnim;
    AudioSource frogAudio;

    public AudioClip ribbitClip;

    void Start()
    {
        frogAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            frogAnim.SetTrigger("doRibbit");
            frogAudio.PlayOneShot(ribbitClip);
        }
    }
}
