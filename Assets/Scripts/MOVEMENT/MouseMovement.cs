using System.Collections;
using System.Collections.Generic;


//public class MouseMovement : MonoBehaviour
//{
    

    //public float mouseSensitivity = 100f;
 
    //float xRotation = 0f;
    //float YRotation = 0f;
 
    //void Start()
    //{
      //Locking the cursor to the middle of the screen and making it invisible
      //Cursor.lockState = CursorLockMode.Locked;
    //}
 
    //void Update()
    //{
      //if (!InventorySystem.Instance.isOpen) {
       //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //control rotation around x axis (Look up and down)
       //xRotation -= mouseY;
 
       //we clamp the rotation so we cant Over-rotate (like in real life)
       //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
       //control rotation around y axis (Look up and down)
       //YRotation += mouseX;
 
       //applying both rotations
       //transform.localRotation = Quaternion.Euler(xRotation,YRotation, 0f);
       
      //}
   // }
//}
//NUOVO CODICE DEL MOUSE MOVEMENT
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilità del mouse
    
    private float xRotation = 0f;

    void Start()
    {
        // Blocca il cursore al centro dello schermo e lo rende invisibile
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Ottieni il movimento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Gestisci la rotazione verticale (asse X)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotazione verticale

        // Applica la rotazione verticale
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0f);

        // Gestisci la rotazione orizzontale (asse Y)
        transform.Rotate(Vector3.up * mouseX, Space.World);
    }
}