using UnityEngine;

public class PokeBehavior : MonoBehaviour
{

    public enum Types
    {
        Dark,
        Poison
    }

    public Types myType;

    public Transform target;

    Animator myAnimator;

    Vector2 dir;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Cleffa").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target.name == "Cleffa") CheckOtherTarget();
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f * Time.deltaTime);
        SetAnimation();
    }
    
    void SetAnimation()
    {
        Vector2 tempDir = (target.position - transform.position).normalized;
        if (tempDir != dir)
        {
            dir = tempDir;
            myAnimator.SetFloat("xVel", dir.x);
            myAnimator.SetFloat("yVel", dir.y);
        }
    }

    void CheckOtherTarget()
    {
        GameObject[] newTargets = GameObject.FindGameObjectsWithTag("Sub");
        for (int i = 0; i < newTargets.Length; i++)
        {
            if (!newTargets[i].GetComponent<SubstituteBehavior>().inUse)
            {
                target = newTargets[i].transform;
                target.GetComponent<SubstituteBehavior>().inUse = true;
            }
        }
    }
}
