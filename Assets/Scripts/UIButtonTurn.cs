using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonTurn : MonoBehaviour
{
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    public float rotationSpeed = 5f;
    public float turnAngle = -45f;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0f, 0f, turnAngle);
    }

    void OnMouseEnter()
    {
        // Smoothly rotate towards the target rotation
        StartCoroutine(RotateSmoothly(targetRotation));
    }

    void OnMouseExit()
    {
        // Smoothly rotate back to the original rotation
        StartCoroutine(RotateSmoothly(originalRotation));
    }

    IEnumerator RotateSmoothly(Quaternion target)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, target, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = target; // Ensure the final rotation is exactly the target rotation
    }
}
