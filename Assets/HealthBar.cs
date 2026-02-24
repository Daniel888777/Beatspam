using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetMaxHealth(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;

    }

    public void setCurrentHealth(int currentHealth)
    {
        healthBarSlider.value = currentHealth;
    }
}
