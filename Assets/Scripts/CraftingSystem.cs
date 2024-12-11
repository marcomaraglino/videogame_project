using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    Button toolsBtn;
    //Craft Buttons
    Button craftAxeBtn;
    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;
    bool isOpen;

    //All Blueprint
    public ItemBlueprint AxeBLP;


    public static CraftingSystem Instance { get; set; }


    private void Awake () {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }   
    }
    // Start is called before the first frame update
    void Start()
    {
        AxeBLP = new ItemBlueprint("asciapietra", 1, "pietra", 3, "Stick", 2);
        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory();});

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(AxeBLP);});
    }

    private void CraftAnyItem(ItemBlueprint blueprint)
    {
        //add item into inventory
        Debug.Log(blueprint.itemName);
        InventorySystem.Instance.AddToInventory(blueprint.itemName);

        if (blueprint.numOfRequirements == 1) {
            InventorySystem.Instance.RemoveItem(blueprint.Req1, blueprint.Req1Amount);
        } else {
            InventorySystem.Instance.RemoveItem(blueprint.Req1, blueprint.Req1Amount);
            InventorySystem.Instance.RemoveItem(blueprint.Req2, blueprint.Req2Amount);
        }

        StartCoroutine(calculate());

        RefreshNeededItems();

        //remove resources from inventory

        //refresh list
    }

    public IEnumerator calculate() {
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCalculateList();

    }

    void OpenToolsCategory() {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
		    Debug.Log("i is pressed");
            craftingScreenUI.SetActive(true);
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            isOpen = false;
        }
        
    }

    private void RefreshNeededItems() {
        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList) {
            switch (itemName) {
                case "pietra":
                    stone_count++;
                    break;
                case "Stick":
                    stick_count++;
                    break;
            }
        }

        // -- AXE -- //
        AxeReq1.text = "3 Pietra [" + stone_count + "]";
        AxeReq2.text = "2 Legnetti [" + stick_count + "]";

        if (stone_count >= 2) {
            craftAxeBtn.gameObject.SetActive(true);
        } else {
            craftAxeBtn.gameObject.SetActive(false);
        }
    }
}
