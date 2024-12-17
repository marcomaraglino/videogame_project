using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearState : MonoBehaviour
{
    public int health = 100;

    // Method to inflict damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Trigger death animation
            // Assuming you have an Animator component attached
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isAttacking", false);
                animator.SetTrigger("Die"); // Replace "Die" with your actual trigger name
            }

            // Destroy the bear after a delay to allow the animation to play
            Destroy(gameObject, 20f); // Adjust the delay as needed
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
