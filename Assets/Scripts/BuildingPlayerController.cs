using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = 30f;

    private CharacterController characterController;
    public Vector3 playerVelocity;

    public bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        //HandleJump();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerVelocity.x = horizontalInput * moveSpeed;

        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Apply gravity
        if (!isGrounded)
        {
            playerVelocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = -2f; // Reset velocity when grounded to avoid accumulating gravity
        }

        // Move the player
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            playerVelocity.y = Mathf.Sqrt(2f * jumpForce * gravity);
        }
        else if(!isGrounded && Input.GetKeyDown(KeyCode.S))
        {
            playerVelocity.y = -500f;
        }

        // Move the player
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
