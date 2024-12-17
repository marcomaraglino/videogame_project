using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }

    public GameObject menuCanvas;
<<<<<<< Updated upstream
=======
    public GameObject postProcessBlur;
    
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
            if (InventorySystem.Instance.isOpen == true) 
            {
                InventorySystem.Instance.inventoryScreenUI.SetActive(false);
                CraftingSystem.Instance.craftingScreenUI.SetActive(false);
                InventorySystem.Instance.isOpen = false;
            }
            EquipSystem.Instance.quickSlotsPanel.SetActive(false);
            EquipSystem.Instance.numberHolder.SetActive(false);
            postProcessBlur.SetActive(true);
>>>>>>> Stashed changes
            menuCanvas.SetActive(true);
            postProcessVolume.SetActive(true);
            menu.SetActive(true);

            Time.timeScale = 0f;

            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

<<<<<<< Updated upstream
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

=======
            Time.timeScale = 0f;


>>>>>>> Stashed changes
        }else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen)
        {

            saveMenu.SetActive(false);
            settingsMenu.SetActive(false);
<<<<<<< Updated upstream
           
            postProcessVolume.SetActive(false);

            Time.timeScale = 1f;

=======
            menu.SetActive(true);
            EquipSystem.Instance.quickSlotsPanel.SetActive(true);
            EquipSystem.Instance.numberHolder.SetActive(true);

            postProcessBlur.SetActive(false);
>>>>>>> Stashed changes
            menuCanvas.SetActive(false);

            isMenuOpen = false;

<<<<<<< Updated upstream
            if (InventorySystem.Instance.isOpen == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
=======
            Cursor.lockState = CursorLockMode.Locked;   
            Cursor.visible = false;

            Time.timeScale = 1f;

>>>>>>> Stashed changes
        }
    }
}
