using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Ensure the player reference is set
        if (player == null)
        {
            Debug.LogError("Player reference not set for EnemyAI script on " + gameObject.name);
        }
    }

    void Update()
    {
        // Check if the player reference is valid
        if (player != null)
        {
            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0f;
    }
}
