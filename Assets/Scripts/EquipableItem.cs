using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

     if(Input.GetMouseButton(0) && InventorySystem.Instance.isOpen == false) // da aggiungere dopo il crafting system (&& CraftingSystem.Instance.isOpen == false)//Left mouse button 
     {
        animator.SetTrigger("hit");
     }
        else{
            animator.ResetTrigger("hit");
        }

    }
}
