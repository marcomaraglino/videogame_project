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

        Vector3 velocity;

        bool isGrounded;
        bool isInWater;

        // Update is called once per frame
        void Update()
        {
            // Controlla se il giocatore è a terra
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            // Controlla se il giocatore è in acqua
            isInWater = Physics.CheckSphere(groundCheck.position, groundDistance, waterMask);

            // Reset della velocità di caduta se il giocatore è a terra
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Movimento del giocatore
            Vector3 move = transform.right * x + transform.forward * z;

            if (!isInWater) // Movimento normale a terra
            {
                controller.Move(move * speed * Time.deltaTime);

                // Salto se il giocatore è a terra
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            else // Movimento in acqua
            {
                controller.Move(move * swimSpeed * Time.deltaTime);

                // Risalita mentre si tiene premuto il tasto di salto
                if (Input.GetButton("Jump"))
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