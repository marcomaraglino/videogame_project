using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHealthBar: MonoBehaviour
{
    private Slider slider;
    private float currentHealth;
    private float maxHealth;
    public GameObject globalState;
    private void Awake()
    {
        slider= GetComponent<Slider>();
    }
    private void Update()
    {
            currentHealth = globalState.GetComponent<GlobalState>().resourceHealth;
            maxHealth = globalState.GetComponent<GlobalState>().resourceMaxHealth;

            float fillValue = currentHealth / maxHealth; // es: 10 / 100 = 0.1
            slider.value = fillValue;
     }
    
}
