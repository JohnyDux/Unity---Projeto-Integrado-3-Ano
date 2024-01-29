using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeTrap : MonoBehaviour
{
    public float freezeDuration; // Adjust this value for the duration of the freeze
    private bool isActive = false;
    public MazeAIChaser AICharacter;
    public BoxCollider boxCollider;
    public MeshRenderer meshRenderer;

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

        if (other.CompareTag("AI"))
        {
            Destroy(meshRenderer);
            Destroy(boxCollider);
        }
    }

    void ActivateFreezeTrap(GameObject aiObject)
    {
        Debug.Log("AI Hit Trap");

        isActive = true;

        // Disable the AI's NavMeshAgent to stop its movement
        AICharacter.isFreezed = true;

        AICharacter.spriteRenderer.sprite = AICharacter.frozenSprite;

        // Start a coroutine to unfreeze the AI after a certain duration
        StartCoroutine(UnfreezeAfterDelay(aiObject));
    }

    IEnumerator UnfreezeAfterDelay(GameObject aiObject)
    {
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable the AI's NavMeshAgent to resume its movement
        AICharacter.isFreezed = false;
        AICharacter.currentPauseTime = 0f;

        AICharacter.spriteRenderer.sprite = AICharacter.normalSprite;

        // Reset the trap state
        isActive = false;
    }
}
