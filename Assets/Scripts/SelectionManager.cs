using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class SelectionManager : MonoBehaviour
{
 
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    public Image centerDotImage;
    public Image handIcon;
 
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }
 
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            var selectionTransform = hit.transform;
 
            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                selectionTransform.GetComponent<InteractableObject>().IfPickedUp();
                interaction_Info_UI.SetActive(true);
            }
            else 
            { 
                interaction_Info_UI.SetActive(false);
            }
 
        } else {
            interaction_Info_UI.SetActive(false);
        }
    }
}