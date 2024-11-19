using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class SelectionManager : MonoBehaviour
{
 
    
    
    public GameObject info;
    
    private void Start()
    {
        
        
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
                
                info.SetActive(true);
               
            }
            else 
            { 
                
                info.SetActive(false);
            }
 
        } else {
            
            info.SetActive(false);
            
        }
    }
}