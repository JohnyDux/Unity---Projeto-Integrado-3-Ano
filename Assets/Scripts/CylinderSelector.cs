using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSelector : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    public float movementSpeed = 5f;
    public GameObject movableObject;

    //Bounds
    [Header("Bounds")]
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    //Raycast
    [Header("Raycast")]
    public LayerMask targetLayer;
    public float maxRayDistance;
    public GameObject hitObject;

    //Snap
    [Header("Grid")]
    public float snapValue = 1f;
    public int gridResolution = 5;

    public float offsetX = 2.0f; // Grid center
    public float offsetY = 1.0f; // Grid center

    private Vector3 gridCenter;

    //Select
    public bool isSelected;

    enum MovementAxis
    {
        None,
        X,
        Y
    }

    private MovementAxis currentMovementAxis = MovementAxis.None;

    void Start()
    {
        // Initialize the grid center
        gridCenter = transform.position;

        isSelected = false;
    }

    void Update()
    {
        //MOVEMENT
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the raw movement direction
        Vector3 rawMovement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        // Snap the movement to the grid
        Vector3 snappedMovement = SnapToGrid(rawMovement);

        // Move the separate GameObject using WASD keys
        MoveObjectWithInput();

        // Visualize the snap steps with debug lines
        DrawSnapGrid();

        // Restrict the position along the X and Z axes
        RestrictPosition();

        //RAYCAST
        // Shoot a ray from the object's position in the forward direction (Z-axis)
        Ray ray = new Ray(transform.position, transform.forward);

        // Declare a RaycastHit variable to store information about the hit
        RaycastHit hit;

        // Check if the ray hits any collider in the specified layer
        if (Physics.Raycast(ray, out hit, maxRayDistance, targetLayer))
        {
            // Access the GameObject that was hit
            hitObject = hit.collider.gameObject;
        }
        else
        {
            // Reset the hitObject variable if nothing is hit
            hitObject = null;
        }

        if(hitObject!= null)
        {
            ChangeObjectColor(Color.yellow);

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                 // Toggle the isSelected state
                isSelected = !isSelected;
            }
            if (isSelected == false)
            {
                ChangeObjectColor(Color.yellow);
            }
            else if (isSelected == true)
            {
                ChangeObjectColor(Color.red);
            }
        }
        else
        {
            ChangeObjectColor(Color.blue);

            isSelected = false;
        }

        // Draw the ray as a line in the game world
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.red);
    }

    void MoveObjectWithInput()
    {
        if (movableObject != null && isSelected == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Determine the dominant movement axis
            if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
            {
                currentMovementAxis = MovementAxis.X;
            }
            else if (Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal))
            {
                currentMovementAxis = MovementAxis.Y;
            }
            else
            {
                currentMovementAxis = MovementAxis.None;
            }

            // Adjust the speed factor
            float speedFactor = 3.0f; // Adjust this value as needed

            // Calculate the adjusted movement based on the grid
            Vector3 movement = Vector3.zero;

            switch (currentMovementAxis)
            {
                case MovementAxis.X:
                    movement.x = moveHorizontal * snapValue * speedFactor * Time.deltaTime;
                    break;
                case MovementAxis.Y:
                    movement.y = moveVertical * snapValue * speedFactor * Time.deltaTime;
                    break;
            }

            // Move the object by the adjusted movement
            movableObject.transform.Translate(movement);

            // Update the grid center based on the movement
            gridCenter += movement;

            // Keep the grid center fixed in the world position
            transform.position = gridCenter;
        }
    }

    void RestrictPosition()
    {
        // Clamp the position to stay within the specified range
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Update the position
        transform.position = new Vector3(clampedX, clampedY, 76);
    }

    Vector3 SnapToGrid(Vector3 input)
    {
        // Snap the x and y values to the nearest multiple of snapValue
        float snappedX = Mathf.Round(input.x / snapValue) * snapValue;
        float snappedY = Mathf.Round(input.y / snapValue) * snapValue;

        // Return the new snapped vector
        return new Vector3(snappedX, snappedY, 0f);
    }

    void DrawSnapGrid()
    {
        // Calculate the half-size of the grid
        float halfGridSize = (gridResolution - 1) * 0.5f * snapValue;

        // Calculate the corners of the camera's view in world space
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Calculate the boundaries of the grid within the camera view
        float minX = gridCenter.x - halfGridSize;
        float maxX = gridCenter.x + halfGridSize;
        float minY = gridCenter.y - halfGridSize;
        float maxY = gridCenter.y + halfGridSize;

        // Draw vertical lines
        for (int i = 0; i < gridResolution; i++)
        {
            float x = Mathf.Lerp(minX, maxX, (float)i / (gridResolution - 1));
            x = Mathf.Clamp(x, bottomLeft.x, topRight.x);
            Debug.DrawLine(new Vector3(x, minY, 0f), new Vector3(x, maxY, 0f), Color.blue);
        }

        // Draw horizontal lines
        for (int j = 0; j < gridResolution; j++)
        {
            float y = Mathf.Lerp(minY, maxY, (float)j / (gridResolution - 1));
            y = Mathf.Clamp(y, bottomLeft.y, topRight.y);
            Debug.DrawLine(new Vector3(minX, y, 0f), new Vector3(maxX, y, 0f), Color.blue);
        }
    }
    void ChangeObjectColor(Color color)
    {
        // Check if the object has a Renderer component
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Change the object's color
            renderer.material.color = color;
        }
    }

}
