using UnityEngine;
using UnityEngine.UI;

public class SliderColors : MonoBehaviour
{

    [SerializeField] Color maxColor, minColor;
    Slider myslider;
    Image sliderImg;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myslider = GetComponent<Slider>();
        sliderImg = myslider.targetGraphic as Image;
    }

    // Update is called once per frame
    void Update()
    {
        sliderImg.color = Color.Lerp(minColor, maxColor, myslider.value);
    }
}
