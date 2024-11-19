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
    
    // Start is called before the first frame update
    void Awake()
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

    void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration() {
        while (true) {
            currentHydration -= 1;
            yield return new WaitForSeconds(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += Vector3.Distance(player.transform.position, lastPosition);
        lastPosition = player.transform.position;
        if (distanceTraveled >= 5) {
            distanceTraveled = 0;
            currentCalories -= 1;
        }
    }
}
