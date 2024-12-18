using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }

    public GameObject menuCanvas;
    public GameObject postProcessBlur;
    

    public GameObject postProcessVolume;
    public GameObject saveMenu;
    public GameObject settingsMenu;
    public GameObject menu;

    public bool isMenuOpen;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen)
        {
            if (InventorySystem.Instance.isOpen == true) 
            {
                InventorySystem.Instance.inventoryScreenUI.SetActive(false);
                CraftingSystem.Instance.craftingScreenUI.SetActive(false);
                InventorySystem.Instance.isOpen = false;
            }
            EquipSystem.Instance.quickSlotsPanel.SetActive(false);
            EquipSystem.Instance.numberHolder.SetActive(false);
            postProcessBlur.SetActive(true);
            menuCanvas.SetActive(true);
            postProcessVolume.SetActive(true);
            menu.SetActive(true);

            Time.timeScale = 0f;

            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0f;


        }else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen)
        {

            saveMenu.SetActive(false);
            settingsMenu.SetActive(false);
            menu.SetActive(true);
            EquipSystem.Instance.quickSlotsPanel.SetActive(true);
            EquipSystem.Instance.numberHolder.SetActive(true);

            postProcessBlur.SetActive(false);
            menuCanvas.SetActive(false);

            isMenuOpen = false;

            Cursor.lockState = CursorLockMode.Locked;   
            Cursor.visible = false;

            Time.timeScale = 1f;

        }
    }
}
