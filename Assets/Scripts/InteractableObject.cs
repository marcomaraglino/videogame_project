// Assets/Scripts/InteractableObject.cs
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isPickable;

    void Update()
    {
        IfPickedUp();
    }
    public void IfPickedUp() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isPickable) {

            if (!InventorySystem.Instance.isFull) {

            Destroy(gameObject);
            }
            else {
                Debug.Log("Inventory full");
            }
        }
    }
}