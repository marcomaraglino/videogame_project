using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class SelectionManager : MonoBehaviour
{
 
    
    
    public Image hand;
    public Image pointer;
    public Vector3 handVelocity;
    
    private void Start()
    {
        pointer.gameObject.SetActive(true);
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
                hand.gameObject.SetActive(true);
                pointer.gameObject.SetActive(false);

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
    
     
}

