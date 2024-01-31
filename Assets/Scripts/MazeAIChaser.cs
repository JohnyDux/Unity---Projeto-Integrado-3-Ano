using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float wanderRadius = 10f; // Adjust this value for the wandering radius
    public float navMeshSpeed = 2.5f;
    public float followDelay = .2f;

    public FieldOfView fieldOfView;
    public Transform player;

    public Vector3 destination;

    void Start()
    {
        // Set the initial destination
        SetRandomDestination();

        navMeshAgent.speed = navMeshSpeed;
    }

    void Update()
    {
        if (fieldOfView.visibleTargets.Count>0)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else if (!navMeshAgent.hasPath)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        // Generate a random point within the wandering radius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit navMeshHit;

        // Find a point on the NavMesh within the generated random direction
        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, wanderRadius, NavMesh.AllAreas))
        {
            destination = navMeshHit.position;

            // Set the NavMeshAgent's destination to the sampled point
            navMeshAgent.SetDestination(navMeshHit.position);
        }
    }
}
