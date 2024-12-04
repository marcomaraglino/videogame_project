using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
 
public class InventorySystem : MonoBehaviour
{
 
   public static InventorySystem Instance { get; set; }
 
    public GameObject inventoryScreenUI;

    public List<GameObject> slotList = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isOpen;
    public bool isFull;

    //Pickup popup
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    
    private CanvasGroup alertCanvasGroup;
    public float fadeDuration = 0.5f;
    public MOVIMENTGIOCATORE playerMovement; // Reference to the MOVIMENTGIOCATORE instance


    // Add this line if it doesn't exist
    public GameObject ItemInfoUI; // Reference to the UI GameObject for item info

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
 
 
    void Start()
    {
        isOpen = false;

        PopulateSlotList();
        alertCanvasGroup = pickupAlert.GetComponent<CanvasGroup>();
    }

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform) {
            if (child.CompareTag("Slot")) {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
		    Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerMovement.SetCanMove(false);

            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = true;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerMovement.SetCanMove(true);

            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = false;
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    void TriggerPickupAlert(string itemName, Sprite itemImage) {
        Debug.Log("Triggering pickup alert for " + itemName);
        pickupName.text = itemName;
        pickupImage.sprite = itemImage;
        pickupAlert.SetActive(true);
        alertCanvasGroup.alpha = 1;
        StopAllCoroutines();
        StartCoroutine(FadeAlert());
    }

    IEnumerator FadeAlert() {
        yield return new WaitForSeconds(3f);

        float elapsedTime = 0;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            alertCanvasGroup.alpha = newAlpha;
            yield return null;
        }

        alertCanvasGroup.alpha = 0;
        pickupAlert.SetActive(false);
    }

    public void AddToInventory(string itemName) {
        if (CheckIfFull()) {
            isFull = true;
            Debug.Log("Inventory full");
        } else {
            whatSlotToEquip = FindNextEmptySlot();

            itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            itemList.Add(itemName);

            TriggerPickupAlert(itemName, itemToAdd.GetComponent<Image>().sprite);
        }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList) {
            if (slot.transform.childCount == 0) {
                return slot;
            }
        }
        return null;
    }

    private bool CheckIfFull()
    {
        foreach (GameObject slot in slotList) {
            if (slot.transform.childCount == 0) {
                return false;
            }
        }
        return true;  
    }
}