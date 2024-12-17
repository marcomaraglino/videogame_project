using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{

    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxHealth;
    public float treeHealth;

    public Animator animator;

    public float caloriesSpentChoppingWood = 20;
    public bool isPickable;
    public string itemName;


    private void Start()
    {
        treeHealth = treeMaxHealth;
        animator = transform.parent.transform.parent.GetComponent<Animator>();
    }

    public void IfPickedUp()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isPickable)
        {
            if (!InventorySystem.Instance.isFull)
            {
                InventorySystem.Instance.AddToInventory(itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    public void GetHit()
    {

        animator.SetTrigger("shake");

        treeHealth -= 1;
        PlayerState.Instance.currentCalories -= caloriesSpentChoppingWood;


        if (treeHealth <= 0)
        {
            TreeIsDead();
        }

    }


    void TreeIsDead()
    {
        Vector3 treePosition = transform.position;

        Destroy(transform.parent.transform.parent.gameObject);
        canBeChopped = false;
        SelectionManager.Instance.selectedTree = null;

        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"),
            new Vector3(treePosition.x, treePosition.y + 1, treePosition.z), Quaternion.Euler(0, 0, 0));

    }

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }

}
