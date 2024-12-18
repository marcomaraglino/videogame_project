using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeState : MonoBehaviour
{

    public int maxHealth = 30;

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
