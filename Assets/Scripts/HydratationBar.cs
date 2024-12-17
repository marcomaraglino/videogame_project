using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HydratationBar : MonoBehaviour
{
    private Slider hydrationSlider;
    public GameObject playerState;
    
    private float currentHydration, maxHydration;
    void Awake()
    {
        hydrationSlider = GetComponent<Slider>();
    }

    void Update()
    {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydration;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydration;

        float fillValue = currentHydration / maxHydration; // es: 10 / 100 = 0.1
        hydrationSlider.value = fillValue;
    }
}
