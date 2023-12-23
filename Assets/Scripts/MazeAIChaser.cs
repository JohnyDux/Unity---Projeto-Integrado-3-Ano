using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public NavMeshAgent navMeshAgent;

    void Start()
    {

        // Ensure the player reference is set
        if (player == null)
        {
            player.GetComponent<MazePlayerController>();
        }
    }

    void Update()
    {
        // Check if the player reference is valid
        if (player != null)
        {
            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.updateRotation = false;
        }

        if(player.transform.position.magnitude - this.transform.position.magnitude < 10)
        {
            navMeshAgent.speed = 4f;
        }
        else if (player.transform.position.magnitude - this.transform.position.magnitude > 10)
        {
            navMeshAgent.speed = 2.5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0f;
    }
}
