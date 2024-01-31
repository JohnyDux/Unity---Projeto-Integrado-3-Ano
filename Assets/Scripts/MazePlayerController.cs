using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    //Movement
    public float moveSpeed = 5f;

    public Rigidbody rb;

    Vector3 movement;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
