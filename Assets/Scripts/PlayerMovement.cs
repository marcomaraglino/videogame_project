using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 12f;
        public float swimSpeed = 6f; // Velocità ridotta per il nuoto
        public float gravity = -9.81f * 2;
        public float waterGravity = -2f; // Gravità ridotta in acqua
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        public LayerMask waterMask; // Maschera per rilevare l'acqua

        public Transform waterSurface; // Punto che indica la superficie dell'acqua
        public float maxSwimHeight = 0.5f; // Altezza massima sopra la superficie dell'acqua

        Vector3 velocity;
        bool isGrounded;
        bool isInWater;

        // Update is called once per frame
        void Update()
        {
            // Controllo se il giocatore è a terra o in acqua
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            isInWater = Physics.CheckSphere(groundCheck.position, groundDistance, waterMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (isInWater)
            {
                // Movimenti in acqua
                controller.Move(move * swimSpeed * Time.deltaTime);

                // Limite della risalita in acqua
                float maxHeight = waterSurface.position.y + maxSwimHeight;

                if (Input.GetButton("Jump") && transform.position.y < maxHeight)
                {
                    // Consente di salire solo fino al limite della superficie
                    velocity.y = swimSpeed * 0.5f; // Risalita controllata
                }
                else if (transform.position.y >= maxHeight)
                {
                    // Se il giocatore è troppo in alto, lo manteniamo alla superficie
                    velocity.y = Mathf.Min(velocity.y, 0);
                }
                else
                {
                    // Applicazione della gravità in acqua
                    velocity.y += waterGravity * Time.deltaTime;
                }
            }
            else
            {
                // Movimenti a terra o in aria
                controller.Move(move * speed * Time.deltaTime);

                // Salto se il giocatore è a terra
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                velocity.y += gravity * Time.deltaTime;
            }

            controller.Move(velocity * Time.deltaTime);
        }
    }
}