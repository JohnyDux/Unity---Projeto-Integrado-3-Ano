using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0f;
    }
}
