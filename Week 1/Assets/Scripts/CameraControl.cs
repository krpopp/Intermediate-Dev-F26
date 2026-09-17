using UnityEngine;

public class CameraControl : MonoBehaviour
{

    float lockX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lockPos = transform.position;
        lockPos.x = lockX;
        transform.position = lockPos;
    }
}
