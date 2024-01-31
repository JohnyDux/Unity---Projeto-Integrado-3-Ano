using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    //Movement
    public Rigidbody rb;
    Vector3 movement;
    public float movementSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minZ = -5f;
    public float maxZ = 5f;

    public int lifes = 3;

    public Transform Mesh;

    //Steps
    public ParticleSystem steps;
    ParticleSystem.MainModule mainModule;
    ParticleSystem.EmissionModule emissionModule;

    public float rateMultiplier;

    Vector3 targetRotationEulerAngles;
    Quaternion targetRotation;
    public float rotationSpeed = 180f;

    //Red Triangle
    public SpriteRenderer triangleRenderer;

    void Start()
    {
        mainModule = steps.main;
        emissionModule = steps.emission;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        MovePlayer(movement);

        // Flip player when x change, use only if you want
        Vector3 characterScale = transform.localScale;
        if (movement.x < 0)
        {
            characterScale.x = -1f;
            Mesh.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            SetNewStartRotation();
            
        }
        else if (movement.x > 0)
        {
            characterScale.x = 1f;
            SetNewStartRotation();
        }
        if (movement.z < 0)
        {
            characterScale.x = -1f;
            SetNewStartRotation();
            
        }
        else if (movement.z > 0)
        {
            characterScale.x = 1f;
            SetNewStartRotation();
        }
        transform.localScale = characterScale;

        //Rate of steps
        if (IsRigidbodyStopped())
        {
            // Rigidbody is considered stopped
            rateMultiplier = 0f;
            triangleRenderer.enabled = true;
        }
        else
        {
            // Rigidbody is still moving
            rateMultiplier = 4f;
            triangleRenderer.enabled = false;
        }
        emissionModule.rateOverTimeMultiplier = rateMultiplier;
    }

    void MovePlayer(Vector3 movement)
    {
        // Calculate the new position
        Vector3 newPosition = rb.position + movement * movementSpeed * Time.deltaTime;

        // Clamp the new position to stay within the specified range
        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedZ = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Update the Rigidbody's position directly
        rb.position = new Vector3(clampedX, rb.position.y, clampedZ);
    }

    void SetNewStartRotation(float rotationSpeed = 180f)
    {
        // Get input for direction
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the target rotation based on input
        targetRotationEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg);

        // Smoothly rotate towards the target rotation
        targetRotation = Quaternion.Euler(targetRotationEulerAngles);

        mainModule.startRotationZ = new ParticleSystem.MinMaxCurve(0f, targetRotation.eulerAngles.z);
    }

    bool IsRigidbodyStopped()
    {
        // Check if the magnitude of the velocity is close to zero
        return movement == Vector3.zero;
    }
}
