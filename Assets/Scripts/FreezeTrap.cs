using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeTrap : MonoBehaviour
{
    public float freezeDuration; // Adjust this value for the duration of the freeze
    private bool isActive = false;
    public MazeAIChaser AICharacter;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
            return;

        // Check if the trigger collides with an object on the AI layer
        if (other.CompareTag("AI"))
        {
            // Trigger the freeze trap
            ActivateFreezeTrap(other.gameObject);
            UnfreezeAfterDelay(other.gameObject);
        }
    }

    void ActivateFreezeTrap(GameObject aiObject)
    {
        Debug.Log("AI Hit Trap");

        isActive = true;

        // Disable the AI's NavMeshAgent to stop its movement
        AICharacter.isFreezed = true;

        // Start a coroutine to unfreeze the AI after a certain duration
        StartCoroutine(UnfreezeAfterDelay(aiObject));
    }

    IEnumerator UnfreezeAfterDelay(GameObject aiObject)
    {
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable the AI's NavMeshAgent to resume its movement
        AICharacter.isFreezed = false;

        // Reset the trap state
        isActive = false;

        // Optionally, play an effect or sound to indicate the end of the freeze
        // Example: GetComponent<ParticleSystem>().Stop();
    }
}
