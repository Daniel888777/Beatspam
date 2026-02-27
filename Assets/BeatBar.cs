using UnityEngine;
using UnityEngine.UI;

public class BeatBar : MonoBehaviour
{
    public Slider beatBarSlider;
    private LaserShootingPlayer laserShooting;
    private bool isFull = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        laserShooting = FindFirstObjectByType<LaserShootingPlayer>();
        beatBarSlider.value = 0f;
        beatBarSlider.maxValue = 10000f;
    }

    public void IncreaseBeatEnergy()
    {
        if (!isFull)
        {
            beatBarSlider.value += 100f;
        }
    }

    public void ConsumeBeat()
    {
        if (beatBarSlider.value > 0f)
        {
            beatBarSlider.value -= (10000f *Time.deltaTime);
        }
    }


    private void Update()
    {
        if (beatBarSlider.value >= beatBarSlider.maxValue)
        {
            beatBarSlider.value = beatBarSlider.maxValue;
            isFull = true;
        }
        else if (beatBarSlider.value <= 0f)
        {
            beatBarSlider.value = 0f;
            isFull = false;
        }

        if (laserShooting != null)
        {
            if (beatBarSlider.value == 10000f)
            {
                laserShooting.LaserFullyCharged();
            }
            else if (beatBarSlider.value == 0f)
            {
                laserShooting.LaserEmpty();
            }

        }
    }



}
