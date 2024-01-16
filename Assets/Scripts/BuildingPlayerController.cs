using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 300f;

    private CharacterController characterController;
    public Vector3 playerVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        playerVelocity.x = horizontalInput * moveSpeed;
        playerVelocity.y = -gravity; // Reset velocity when grounded to avoid accumulating gravity

        // Move the player
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
