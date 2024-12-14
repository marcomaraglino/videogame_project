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
        spawnPoint = player.transform.position;

        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;
        isDead = false;
        deathScreenUI.SetActive(false);

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
        if (!isDead)
        {
            // Check for death condition
            if (currentHealth <= 0)
            {
                Die();
            }

            distanceTraveled += Vector3.Distance(player.transform.position, lastPosition);
            lastPosition = player.transform.position;
            if (distanceTraveled >= 5) {
                distanceTraveled = 0;
                currentCalories -= 1;
            }
        }
    }

    void Die()
    {
        isDead = true;
        deathScreenUI.SetActive(true);
        
        // Disable player movement
        if (player.GetComponent<MOVIMENTGIOCATORE>() != null)
        {
            player.GetComponent<MOVIMENTGIOCATORE>().enabled = false;
        }
        
        // Show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Respawn()
    {
        isDead = false;
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydration = maxHydration;
        InventorySystem.Instance.ClearInventory();
        
        // Enable player movement
        
        // Hide death screen
        deathScreenUI.SetActive(false);
        
        // Reset player position (optional)
        Debug.Log(player.transform.position);
        player.transform.position = spawnPoint;
        Debug.Log("Player position after teleport: " + player.transform.position);

        if (player.GetComponent<MOVIMENTGIOCATORE>() != null)
        {
            player.GetComponent<MOVIMENTGIOCATORE>().enabled = true;
        }
 // Adjust coordinates as needed
        
        // Lock cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
