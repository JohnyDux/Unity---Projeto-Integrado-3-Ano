using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleJumper : MonoBehaviour
{
    public float jumpHeight = 1.0f;    // Adjust this value for the jump height
    public float jumpDuration = 1.0f;  // Adjust this value for the jump duration

    private Coroutine jumpCoroutine;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;

        // Start the jumping coroutine
        jumpCoroutine = StartCoroutine(JumpRoutine());
    }

    private System.Collections.IEnumerator JumpRoutine()
    {
        while (true)
        {
            // Jump up
            yield return Jump(new Vector3(0f, 0f, jumpHeight), jumpDuration);

            // Jump down
            yield return Jump(new Vector3(0f, 0f, -jumpHeight), jumpDuration);
        }
    }

    private System.Collections.IEnumerator Jump(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = startPosition + Vector3.Lerp(Vector3.zero, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final position is exactly the target position
        transform.localPosition = startPosition + targetPosition;
    }

    void OnDestroy()
    {
        // Stop the coroutine when the script is destroyed to prevent memory leaks
        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
        }
    }
}
