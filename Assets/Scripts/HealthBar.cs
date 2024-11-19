using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthSlider;
    public GameObject playerState;
    
    private float currentHealth, maxHealth;
    void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth; // es: 10 / 100 = 0.1
        healthSlider.value = fillValue;
    }
}
