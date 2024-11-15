using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 12f;
        public float gravity = -9.81f * 2;
        public float waterGravity = -4.0f; // Gravità mentre si è in acqua
        public float jumpHeight = 3f;

        public float swimSpeed = 6f; // Velocità di nuoto
        public float diveSpeed = 4f; // Velocità di tuffo
        public float swimRiseSpeed = 2f; // Velocità di risalita
        public LayerMask waterMask; // Maschera per rilevare l'acqua

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        public WaterPhysics waterPhysics; // Riferimento a WaterPhysics

        Vector3 velocity;

        bool isGrounded;
        bool isInWater;

        // Update is called once per frame
        void Update()
        {
            // Controlla se il giocatore è a terra
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            // Controlla se il giocatore è in acqua
            isInWater = waterPhysics != null && waterPhysics.isUnderwater; // Aggiorna il valore di isInWater

            // Reset della velocità di caduta se il giocatore è a terra
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(0, 0, 0);

            // Movimento del giocatore
            if (!InventorySystem.Instance.isOpen) {
                move = transform.right * x + transform.forward * z;
            }

            if (!isInWater) // Movimento normale a terra
            {
                
                controller.Move(move * speed * Time.deltaTime);

                // Salto se il giocatore è a terra
                if (Input.GetButtonDown("Jump") && isGrounded && !InventorySystem.Instance.isOpen)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            else // Movimento in acqua
            {
                Debug.Log("Sei in acqua");
                controller.Move(move * swimSpeed * Time.deltaTime);

                // Risalita mentre si tiene premuto il tasto di salto
                if (Input.GetButton("Jump") && !InventorySystem.Instance.isOpen)
                {
                    velocity.y = swimRiseSpeed; // Risalita controllata
                }
                else
                {
                    // Applicazione della gravità in acqua
                    velocity.y += waterGravity * Time.deltaTime; 
                }
            }

            // Applicazione della gravità normale se non in acqua
            if (!isInWater)
            {
                velocity.y += gravity * Time.deltaTime;
            }

            // Muovi il giocatore
            controller.Move(velocity * Time.deltaTime);
        }
    }
}