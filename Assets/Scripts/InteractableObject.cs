// Assets/Scripts/InteractableObject.cs
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isPickable;
    public string itemName;

    public string GetItemName() {
        return itemName;
    }

    public void IfPickedUp() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isPickable) {
            if (!InventorySystem.Instance.isFull) {
                InventorySystem.Instance.AddToInventory(itemName);
                Destroy(gameObject);
            } else {
                Debug.Log("Inventory full");
            }
        }
    }
}