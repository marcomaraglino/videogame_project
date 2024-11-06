using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject InteractionInfo;
    private TextMeshProUGUI interaction_text;
    public float interactionDistance = 3f; // Distance to show interaction text

    private void Start()
    {
        interaction_text = InteractionInfo.GetComponent<TextMeshProUGUI>();
        InteractionInfo.SetActive(false); // Hide the interaction info at start
    }

void Update()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
        var selectionTransform = hit.transform;
        Debug.Log("Hit object: " + selectionTransform.name); // Log the name of the hit object

        InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
        Debug.Log("Interactable component: " + interactable); // Log the interactable component

        // Check if the player is close enough to the interactable object
        if (interactable && Vector3.Distance(transform.position, selectionTransform.position) <= interactionDistance)
        {
            InteractionInfo.SetActive(true);
            interaction_text.text = interactable.GetItemName();
            Debug.Log("Displaying info for: " + interactable.GetItemName()); // Log when displaying info
        }
        else
        {
            InteractionInfo.SetActive(false);
        }
    }
    else
    {
        InteractionInfo.SetActive(false); // Hide UI if nothing is hit
    }
}
}