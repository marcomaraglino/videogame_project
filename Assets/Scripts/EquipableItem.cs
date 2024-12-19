using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    public float attackRate = 1.5f; // Attack rate in seconds
    private float nextAttackTime = 0f; // Time when the player can attack again

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && InventorySystem.Instance.isOpen == false && Time.time >= nextAttackTime) // Check attack rate
        {
            animator.SetTrigger("hit");
            nextAttackTime = Time.time + attackRate; // Set the next attack time

            //Choppable Tree

            // Check for nearby bears
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 7.1f); // Adjust radius as needed
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Bear"))
                {
                    Debug.Log("Hit bear");
                    BearState bear = hitCollider.GetComponent<BearState>();
                    if (bear != null)
                    {
                        bear.TakeDamage(10); // Inflict damage (adjust damage value as needed)
                    }
                }

                if (hitCollider.CompareTag("Tree"))
                {
                    Debug.Log("Hit tree");
                    TreeState tree = hitCollider.GetComponent<TreeState>();
                    if (tree != null)
                    {
                        tree.TakeDamage(10); // Inflict damage (adjust damage value as needed)
                    }
                    InventorySystem.Instance.AddToInventory("legnetto");
                }

                if (hitCollider.CompareTag("Stone"))
                {
                    Debug.Log("Hit stone");
                    StoneState tree = hitCollider.GetComponent<StoneState>();
                    if (tree != null)
                    {
                        tree.TakeDamage(10); // Inflict damage (adjust damage value as needed)
                    }
                    InventorySystem.Instance.AddToInventory("pietra");
                }
            }
        }
        else
        {
            animator.ResetTrigger("hit");
        }
    }
}
