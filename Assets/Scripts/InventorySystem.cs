using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 
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

    public List<string> itemsPickedup;
    
 
 
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
 
        if (MenuManager.Instance.isMenuOpen == true)
        {
        }
        else
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
                isOpen = false;
            }
        }
    }

    void TriggerPickupAlert(string itemName, Sprite itemImage) {
        Debug.Log("Triggering pickup alert for " + itemName);
        pickupName.text = itemName;
        pickupImage.sprite = itemImage;
        pickupAlert.SetActive(true);
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