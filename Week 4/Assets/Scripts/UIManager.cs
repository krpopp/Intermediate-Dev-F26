using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    public CleffaBehavior cleffaObj;

    public Slider hungerBar;
    public Slider sleepBar;
    public Slider hpBar;
    public Slider xpBar;

    [SerializeField] GameObject[] subImgObjects;
    public List<Image> subIcons = new List<Image>();

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
        for (int i = 0; i < subImgObjects.Length; i++)
        {
            subIcons.Add(subImgObjects[i].GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateSliders()
    {
        hungerBar.value = cleffaObj.fullnessVal / 10;
        sleepBar.value = cleffaObj.sleepinessVal / 10;
        hpBar.value = cleffaObj.hpVal / 10;
        xpBar.value = cleffaObj.xpVal / 10;
    }
    
    public void UpdateSubs(int subIndex, bool changeTo)
    {
        if(subIcons[subIndex] != null) subIcons[subIndex].enabled = changeTo;
    }
}
