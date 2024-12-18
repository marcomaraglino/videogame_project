using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance {get; set;}
    public float currentHealth;
    public float maxHealth;
    public float currentCalories;
    public float maxCalories;
    float distanceTraveled = 0;
    Vector3 lastPosition;
    public GameObject player;
    public float currentHydration;
    public float maxHydration;
    private Vector3 spawnPoint;
    private bool isDead = false;
    public GameObject deathScreenUI; // Assign this in Unity Inspector

    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
