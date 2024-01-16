using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCylinder : MonoBehaviour
{
    [SerializeField]
    public GameObject cylinder;
    public bool correctPosition;

    public Slider RotateSlider;

    //rotation check
    public float targetXRotation = 45f; // Set the desired X rotation here
    public float rotationTolerance = 5f;

    void Start()
    {
        correctPosition = false;
    }

    void Update()
    {
        float snappedX = Mathf.Round(RotateSlider.value / 90.0f) * 90.0f;

        // Rotate the GameObject around the Y-axis based on the yRotation variable
        transform.rotation = Quaternion.Euler(snappedX, 0f, -90f);

        //Check rotation
        float currentXRotation = transform.rotation.eulerAngles.x;
        if (Mathf.Abs(currentXRotation - targetXRotation) <= rotationTolerance)
        {
            correctPosition = true;
        }
        else
        {
            correctPosition = false;
        }
    }
}
