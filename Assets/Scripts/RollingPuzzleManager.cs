using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollingPuzzleManager : MonoBehaviour
{
    //Check correct cylinder
    public TextMeshProUGUI correctPiecesText;
    public RotateCylinder[] cylinders;

    public int correctPieces;

    private bool[] previousBooleanStates;

    public CylinderSelector cylinderSelector;

    //Selector
    public Color frameColor; // Set the color of the frame
    public float frameSize = 100f; // Set the size of the frame
    public float frameThickness = 5f; // Set the thickness of the frame lines
    public Vector3 frameWorldPosition; // Set the world position of the frame

    //Move Selector
    public float moveDistanceX; // Set the fixed distance to move along the x-axis
    public float moveDistanceY; // Set the fixed distance to move along the y-axis

    public float minX = -10f; // Minimum allowed x-axis position
    public float maxX = 10f;  // Maximum allowed x-axis position

    public float minY = -5f;  // Minimum allowed y-axis position
    public float maxY = 5f;   // Maximum allowed y-axis position

    public GameObject activeGameObject;
    public float activeGameObjectRotation;
    public Color activeObjectColor; // Color for the active game object
    public List<GameObject> gameObjects; // List of game objects to check against
    public bool isLocked = false; // Flag to track whether the frame is locked
    bool trigger;
    private bool isSelected = false; // Flag to track if the spacebar has been pressed

    private bool canMoveX = true; // Control X-axis movement
    private bool canMoveY = true; // Control Y-axis movement

    public Slider slider;

    //Time and Win
    [Range(0, 60)] public float timeLeft;
    public TextMeshProUGUI timeText;
    public GameObject WinSprite;
    public GameObject LostSprite;

    void Start()
    {
        frameWorldPosition = new Vector3(0f, 3f, 5f);
        moveDistanceX = 165f;
        moveDistanceY = 205f;

        // Initialize the array to store previous boolean states
        previousBooleanStates = new bool[cylinders.Length];
        for (int i = 0; i < cylinders.Length; i++)
        {
            if (cylinders[i] != null)
            {
                previousBooleanStates[i] = cylinders[i].correctPosition;
            }
        }

        slider.value = 0;

        trigger = false;

        WinSprite.SetActive(false);
        LostSprite.SetActive(false);
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();

            // Check for keyboard input (you can customize this condition based on your requirements)
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                // Disable the slider interactability
                if (slider != null)
                {
                    slider.interactable = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                // Enable the slider interactability when keyboard input is released
                if (slider != null)
                {
                    slider.interactable = true;
                }
            }


            activeGameObject = cylinderSelector.hitObject;

            correctPiecesText.text = "Correct Pieces: " + correctPieces;

            // Check for changes in the boolean property and modify the variable accordingly
            for (int i = 0; i < cylinders.Length; i++)
            {
                if (cylinders[i] != null)
                {
                    if (cylinders[i].correctPosition != previousBooleanStates[i])
                    {
                        // Update the variable based on the change in the boolean property
                        correctPieces += cylinders[i].correctPosition ? 1 : -1;

                        // Update the previous state
                        previousBooleanStates[i] = cylinders[i].correctPosition;
                    }
                }
            }

            // Move the frame based on input
            MoveFrame();

            // Check if the frame's position matches any game object's position
            CheckGameObjectCollision();

            // Toggle spacePressed when the spacebar is pressed
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isSelected = !isSelected;

                if (trigger == true)
                {
                    trigger = false;
                }
                else
                {
                    trigger = true;
                }

                // If space is pressed again, reset the color and selected game object
                if (!isSelected)
                {
                    ResetColors();
                    isLocked = false;
                    activeGameObject = null;
                }
            }

            if (trigger == true)
            {
                isLocked = true;
            }
            else
            {
                isLocked = false;
            }

            if (activeGameObject != null && isLocked)
            {
                float xAngle = slider.value;
                activeGameObjectRotation = xAngle;

                activeGameObject.GetComponent<RotateCylinder>().currentXRotation = activeGameObjectRotation;
            }

            if (correctPieces == 9)
            {
                Time.timeScale = 0f;
                WinSprite.SetActive(true);

                //Slider Lock
                if (slider != null)
                {
                    slider.interactable = false;
                }
            }
        }

        else
        {
            Time.timeScale = 0f;
            LostSprite.SetActive(true);
        }

        
    }

    void OnGUI()
    {
        // Convert the world position to screen space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(frameWorldPosition);

        // Set the color for the frame based on whether it's locked or not
        GUI.color = isLocked ? Color.red : Color.black;

        // Draw the outer frame
        DrawOuterFrame(screenPosition.x, screenPosition.y, frameSize, frameThickness);

        // Reset the color to avoid affecting other GUI elements
        GUI.color = Color.white;
    }

    void DrawOuterFrame(float x, float y, float size, float thickness)
    {
        // Draw the top edge of the outer frame
        GUI.DrawTexture(new Rect(x - size / 2, Screen.height - y - size / 2 - thickness / 2, size, thickness), Texture2D.whiteTexture);
        // Draw the bottom edge of the outer frame
        GUI.DrawTexture(new Rect(x - size / 2, Screen.height - y + size / 2 - thickness / 2, size, thickness), Texture2D.whiteTexture);
        // Draw the left edge of the outer frame
        GUI.DrawTexture(new Rect(x - size / 2, Screen.height - y - size / 2, thickness, size), Texture2D.whiteTexture);
        // Draw the right edge of the outer frame
        GUI.DrawTexture(new Rect(x + size / 2 - thickness, Screen.height - y - size / 2, thickness, size), Texture2D.whiteTexture);
    }

    void MoveFrame()
    {
        // Allow frame movement only if it's not locked
        if (!isLocked)
        {
            // Move the frame by a fixed distance along the x-axis when the corresponding key is pressed
            if (canMoveX)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    frameWorldPosition.x = Mathf.Clamp(frameWorldPosition.x - moveDistanceX, minX, maxX);
                    canMoveY = false; // Disable Y-axis movement while moving along X-axis
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    frameWorldPosition.x = Mathf.Clamp(frameWorldPosition.x + moveDistanceX, minX, maxX);
                    canMoveY = false; // Disable Y-axis movement while moving along X-axis
                }
            }

            // Move the frame by a fixed distance along the y-axis when the corresponding key is pressed
            if (canMoveY)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    frameWorldPosition.y = Mathf.Clamp(frameWorldPosition.y - moveDistanceY, minY, maxY);
                    canMoveX = false; // Disable X-axis movement while moving along Y-axis
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    frameWorldPosition.y = Mathf.Clamp(frameWorldPosition.y + moveDistanceY, minY, maxY);
                    canMoveX = false; // Disable X-axis movement while moving along Y-axis
                }
            }
        }
        else if(isLocked)
        {
            canMoveX = false;
            canMoveY = false;
        }

        // Enable both X and Y-axis movement when no keys are pressed
        if (!Input.anyKey && !isLocked)
        {
            canMoveX = true;
            canMoveY = true;
        }
    }

    void CheckGameObjectCollision()
    {
        Ray ray = new Ray(frameWorldPosition, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (gameObjects.Contains(hitObject))
            {
                // Set the active game object when collision occurs
                activeGameObject = hitObject;

                // Change the color of the selection square (frame)
                if (isLocked == false && activeGameObject != null)
                {
                    frameColor = Color.black;
                    activeGameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                else
                {
                    frameColor = Color.red;
                    activeGameObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            else
            {
                // Reset the color of non-active game objects
                foreach (GameObject obj in gameObjects)
                {
                    obj.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }

    void ResetColors()
    {
        // Reset the color of the selection square (frame)
        frameColor = Color.red;

        // Reset the color of all game objects
        foreach (GameObject obj in gameObjects)
        {
            obj.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}