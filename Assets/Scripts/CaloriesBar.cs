using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{
    private Slider caloriesSlider;
    public GameObject playerState;
    
    private float currentCalories, maxCalories;
    void Awake()
    {
        caloriesSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        float fillValue = currentCalories / maxCalories; // es: 10 / 100 = 0.1
        caloriesSlider.value = fillValue;
    }
}
