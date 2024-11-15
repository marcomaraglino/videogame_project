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
            InventorySystem.Instance.AddToInventory(itemName);
            Destroy(gameObject);
        }
    }
}