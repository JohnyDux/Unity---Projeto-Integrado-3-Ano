using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTest : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minZ = -5f;
    public float maxZ = 5f;

    void Update()
    {
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player
        MovePlayer(movement);
    }

    void MovePlayer(Vector3 movement)
    {
        // Get the Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();

        // Calculate the new position
        Vector3 newPosition = rb.position + movement * movementSpeed * Time.deltaTime;

        // Clamp the new position to stay within the specified range
        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedZ = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Update the Rigidbody's position directly
        rb.position = new Vector3(clampedX, rb.position.y, clampedZ);
    }
}
