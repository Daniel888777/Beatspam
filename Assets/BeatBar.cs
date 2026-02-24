using UnityEngine;
using UnityEngine.UI;

public class BeatBar : MonoBehaviour
{
    public Slider beatBarSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        beatBarSlider.value = 0f;
        beatBarSlider.maxValue = 10000f;
    }

    public void IncreaseBeatEnergy()
    {
        beatBarSlider.value += 0.5f;
    }
}
