using System;
using UnityEngine;

public class GrassWave : MonoBehaviour
{

    public Vector3 waveStart;

    public Vector3 waveEnd;

    float frequency;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frequency = UnityEngine.Random.Range(0.1f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion start = Quaternion.Euler(waveStart);
        Quaternion end = Quaternion.Euler(waveEnd);
        float waveLerp = 0.1f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * frequency));
        transform.localRotation = Quaternion.Lerp(start, end, waveLerp);
    }
}
