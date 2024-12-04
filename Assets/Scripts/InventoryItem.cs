using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //
    public bool isTrashable;
 
    // --- Item Info UI --- //
    private GameObject itemInfoUI;
 
    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;
 
    public string thisName, thisDescription, thisFunctionality;
 
    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;
 
    public float healthEffect;
    public float caloriesEffect;
    public float hydrationEffect;
 
 
    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;

        if (itemInfoUI == null)
        {
            Debug.LogError("ItemInfoUI is not assigned in InventorySystem.");
            return; // Exit if itemInfoUI is null
        }

        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription")?.GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("ItemFunctionality")?.GetComponent<TextMeshProUGUI>();

        if (itemInfoUI_itemName == null)
            Debug.LogError("itemInfoUI_itemName is not found.");
        if (itemInfoUI_itemDescription == null)
            Debug.LogError("itemInfoUI_itemDescription is not found.");
        if (itemInfoUI_itemFunctionality == null)
            Debug.LogError("itemInfoUI_itemFunctionality is not found.");
    }
 
    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }
 
    // Triggered when the mouse exits the area of the item that has this script.
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }
 
    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData)
    {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }
        }
    }
 
    // Triggered when the mouse button is released over the item that has this script.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                //InventorySystem.Instance.ReCalculeList();
                //CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }
 
    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        itemInfoUI.SetActive(false);
 
        healthEffectCalculation(healthEffect);
 
        caloriesEffectCalculation(caloriesEffect);
 
        hydrationEffectCalculation(hydrationEffect);
 
    }
 
 
    private static void healthEffectCalculation(float healthEffect)
    {
        // --- Health --- //
 
        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;
 
        if (healthEffect != 0)
        {
            if ((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.currentHealth = maxHealth;
            }
            else
            {
                PlayerState.Instance.currentHealth += healthEffect;
            }
        }
    }
 
 
    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        // --- Calories --- //
 
        float caloriesBeforeConsumption = PlayerState.Instance.currentCalories;
        float maxCalories = PlayerState.Instance.maxCalories;
 
        if (caloriesEffect != 0)
        {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                PlayerState.Instance.currentCalories = maxCalories;
            }
            else
            {
                PlayerState.Instance.currentCalories += caloriesEffect;
            }
        }
    }
 
 
    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // --- Hydration --- //
 
        float hydrationBeforeConsumption = PlayerState.Instance.currentHydration;
        float maxHydration = PlayerState.Instance.maxHydration;
 
        if (hydrationEffect != 0)
        {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration)
            {
                PlayerState.Instance.currentHydration = maxHydration;
            }
            else
            {
                PlayerState.Instance.currentHydration += hydrationEffect;
            }
        }
    }
 
 
}