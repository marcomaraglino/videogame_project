using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MOVIMENTGIOCATORE : MonoBehaviour
{   
    public Animator playerAnim;
    public Camera playerCamera;
    public float walkSpeed = 10f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    private bool canMove = true;
    private bool isInWater = false;
    public PlayerState playerStateInstance;

    IEnumerator decreaseHealthInWater() {
        yield return new WaitForSeconds(8);
        while (isInWater) {
            yield return new WaitForSeconds(1);
            playerStateInstance.currentHealth -= 1;
        }
    }

    public bool IsInWater() {
        return isInWater;
    }

    private bool walking;

    public bool GetCanMove() {
        return canMove;
    }
    public void SetCanMove(bool canMove) {
        this.canMove = canMove;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.height = defaultHeight;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerAnim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WaterLayer")){
            gravity = 4f;
            isInWater = true;
            StartCoroutine(decreaseHealthInWater());
            // Optionally, you can set the player's height or other properties for swimming
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("WaterLayer")){
            gravity = 20f;
            isInWater = false;
        }
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && (characterController.isGrounded || isInWater))
        {
            if (isInWater)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (characterController.isGrounded && walking)
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
           characterController.height = crouchHeight;
           walkSpeed = crouchSpeed;
           runSpeed = crouchSpeed;

        }
        else
        {
           characterController.height = defaultHeight;
           walkSpeed = 10f;
           runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if(Input.GetKeyDown(KeyCode.W)){
			playerAnim.SetTrigger("walk");
			playerAnim.ResetTrigger("idle");
			walking=true;
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.W)){
			playerAnim.ResetTrigger("walk");
			playerAnim.SetTrigger("idle");
            walking=false;
			//steps1.SetActive(false);
		}
        if(Input.GetKeyDown(KeyCode.Space)){
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");   
           
        }
    }
}