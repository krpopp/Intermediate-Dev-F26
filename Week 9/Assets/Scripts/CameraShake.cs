using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CinemachineBasicMultiChannelPerlin shaker;
    [SerializeField] private float shakeDecay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shaker = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public static void ShakeScreen(float shakePower)
    {
        shaker.AmplitudeGain = shakePower;
        shaker.FrequencyGain = shakePower;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (shaker.AmplitudeGain > 0)
        {
            shaker.AmplitudeGain -= shakeDecay * Time.deltaTime;
            shaker.FrequencyGain -= shakeDecay * Time.deltaTime;
        }
    }
}
