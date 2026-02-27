using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBarSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SetMaxHealth(float maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;

    }

    public void setCurrentHealth(float currentHealth)
    {
        healthBarSlider.value = currentHealth;
    }
}
