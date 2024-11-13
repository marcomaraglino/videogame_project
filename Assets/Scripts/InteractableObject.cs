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
            Debug.Log("item added in inventory");
            Destroy(gameObject);
        }
    }
}