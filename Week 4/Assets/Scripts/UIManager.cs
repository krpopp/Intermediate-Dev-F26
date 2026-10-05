using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static CleffaBehavior cleffaObj;

    public static Slider hungerBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cleffaObj = GameObject.Find("Cleffa").GetComponent<CleffaBehavior>();
        hungerBar = GameObject.Find("Hunger").GetComponent<Slider>();
        hungerBar.value = cleffaObj.fullnessVal / 10;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateSliders()
    {
        hungerBar.value = cleffaObj.fullnessVal / 10;
    }
}
