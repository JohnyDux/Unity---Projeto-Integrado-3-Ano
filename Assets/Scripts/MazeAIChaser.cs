using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float wanderRadius = 10f; // Adjust this value for the wandering radius
    public float wanderInterval = 5f; // Adjust this value for the interval between destination changes
    public float navMeshSpeed = 2.5f;
    public Vector3 previousDestination;

    public GameObject player;
    public float chaseDistance = 10;

    private float nextWanderTime;
    public float minDistanceBetweenDestinations = 100f;
    public bool goalAchieved;

    public bool isFreezed;
    public float currentPauseTime;

    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite frozenSprite;

    void Start()
    {
        // Set the initial destination
        SetRandomDestination();
        goalAchieved = false;

        navMeshAgent.speed = navMeshSpeed;

        isFreezed = false;
        spriteRenderer.sprite = normalSprite;
    }

    void Update()
    {
        // Check if it's time to set a new destination
        if (Time.time >= nextWanderTime && isFreezed == false)
        {
            navMeshAgent.speed = navMeshSpeed;

            if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) > chaseDistance)
            {
                // Check if the agent has reached its destination
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
                {
                    // Set a new random destination
                    SetRandomDestination();
                }
                // Update the next wander time
                nextWanderTime = Time.time + wanderInterval;
            }
            else
            {
                navMeshAgent.SetDestination(player.transform.position);
            }
        }

        if(isFreezed == true)
        {
            currentPauseTime = currentPauseTime + Time.deltaTime;
            navMeshAgent.speed = 0f;
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
            // Set the NavMeshAgent's destination to the sampled point
            navMeshAgent.SetDestination(navMeshHit.position);
        }
    }
}
