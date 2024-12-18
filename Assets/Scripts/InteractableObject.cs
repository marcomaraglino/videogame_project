// Assets/Scripts/InteractableObject.cs
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName;
    public bool isPickable;

    public string GetItemName()
    {
        return itemName;
    }

    public void IfPickedUp() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isPickable) {

            if (!InventorySystem.Instance.isFull) {
                InventorySystem.Instance.AddToInventory(itemName);

                InventorySystem.Instance.itemsPickedup.Add(gameObject.name);

                Destroy(gameObject);
            } else {
                Debug.Log("Inventory full");
            }
        }
    }
}