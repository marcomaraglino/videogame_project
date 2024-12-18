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

            ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();
            if (choppableTree && choppableTree.playerInRange)
            {
                choppableTree.canBeChopped= true;
                selectedTree=choppableTree.gameObject;
            }
            
            else
            {
                if (selectedTree!=null)
                {selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped=false
                selectedTree=null;
                }
            }

 
            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                selectionTransform.GetComponent<InteractableObject>().IfPickedUp();

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);

            // Movimento fluido con SmoothDamp
            hand.rectTransform.position = Vector3.SmoothDamp(
                hand.rectTransform.position, 
                screenPosition, 
                ref handVelocity, 
                0.1f // Tempo di smorzamento
            );
               
            }
            else 
            { 
                hand.gameObject.SetActive(false);
                pointer.gameObject.SetActive(true);
            }
 
        } else {
            hand.gameObject.SetActive(false);
            pointer.gameObject.SetActive(true);
            
        }
    }

    public void DisableSelection()
    {

    }

    public void EnableSelection()
    {

    }
}