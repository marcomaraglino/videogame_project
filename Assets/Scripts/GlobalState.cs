using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalState Instance { get; private set; }
    public float resourceHealth;
    public float resourceMaxHealth;

   
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
