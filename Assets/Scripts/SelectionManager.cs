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


<<<<<<< HEAD
=======
    public Image hand;
    public Image pointer;
    public Vector3 handVelocity;
    public GameObject selectedTree;
    public Vector3 screenPosition;
    
>>>>>>> parent of 0660f59 (simple tree cutting)
    
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
            InteractableObject choppableTree = selectionTransform.GetComponent<InteractableObject>();
            if (selectionTransform.GetComponent<ChoppableTree>())
<<<<<<< HEAD
=======
            {
                hand.gameObject.SetActive(true);
                pointer.gameObject.SetActive(false);

                selectionTransform.GetComponent<ChoppableTree>().IfPickedUp();

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
            }

            if (selectionTransform.GetComponent<InteractableObject>())
>>>>>>> parent of 0660f59 (simple tree cutting)
            {
                hand.gameObject.SetActive(true);
                pointer.gameObject.SetActive(false);

                selectionTransform.GetComponent<ChoppableTree>().IfPickedUp();

                Vector3 screenPosition = Camera.main.WorldToScreenPoint(hit.point);
            }

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