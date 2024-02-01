using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCylinder : MonoBehaviour
{
    [SerializeField]
    public GameObject cylinder;
    public bool correctPosition;

    public RollingPuzzleManager puzzleManager;
    public CylinderSelector cylinderSelector;

    //rotation check
    public float currentXRotation;
    public float targetXRotation = 45f; // Set the desired X rotation here

    void Start()
    {
        correctPosition = false;
    }

    void Update()
    {
        float snappedX = Mathf.Round(puzzleManager.slider.value / 90.0f) * 90.0f;

        currentXRotation = transform.rotation.eulerAngles.x;

        if (this.gameObject == puzzleManager.activeGameObject && cylinderSelector.isSelected && cylinderSelector.hitObject == this.gameObject)
        {
            // Rotate the GameObject around the Y-axis based on the yRotation variable
            cylinder.transform.rotation = Quaternion.Euler(snappedX, 0f, -90f);

            //Check rotation
            if (Mathf.Approximately(Mathf.Abs(currentXRotation), Mathf.Abs(targetXRotation)))
            {
                correctPosition = true;
            }
            else
            {
                correctPosition = false;
            }
        }      
    }
}
