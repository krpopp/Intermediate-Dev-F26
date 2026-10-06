using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static CleffaBehavior cleffaObj;

    public static Slider hungerBar;
    public static Slider sleepBar;
    public static Slider hpBar;
    public static Slider xpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cleffaObj = GameObject.Find("Cleffa").GetComponent<CleffaBehavior>();
        hungerBar = GameObject.Find("Hunger").GetComponent<Slider>();
        sleepBar = GameObject.Find("Sleep").GetComponent<Slider>();
        hpBar = GameObject.Find("HP").GetComponent<Slider>();
        xpBar = GameObject.Find("XP").GetComponent<Slider>();
        hungerBar.value = cleffaObj.fullnessVal / 10;
        sleepBar.value = cleffaObj.sleepinessVal / 10;
        hpBar.value = cleffaObj.hpVal / 10;
        xpBar.value = cleffaObj.xpVal / 10;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateSliders()
    {
        hungerBar.value = cleffaObj.fullnessVal / 10;
        sleepBar.value = cleffaObj.fullnessVal / 10;
    }
}
