// Assets/Scripts/InteractableObject.cs
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;

    public string GetItemName()
    {
        return itemName;
    }
<<<<<<< Updated upstream
=======

    public void IfPickedUp() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isPickable) {

            if (!InventorySystem.Instance.isFull) {


            InventorySystem.Instance.AddToInventory(itemName);
            Destroy(gameObject);
            }
            else {
                Debug.Log("Inventory full");
            }
        }
    }
>>>>>>> Stashed changes
}