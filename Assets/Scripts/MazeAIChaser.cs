using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeAIChaser : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public NavMeshAgent navMeshAgent;
    public float NaveMeshRotationSpeed = 5f;

    //Cone of Vision
    public GameObject ConeOfVision;
    public float coneRotationSpeed;

    //Movement Pause
    public FreezeTrap freezeTrap;
    public bool isFreezed;
    [SerializeField]private float currentPauseTime = 0f;

    //Sprite Manager
    public SpriteRenderer AISpriteRenderer;
    public Sprite normalSprite; // Assign the normal state sprite in the Unity Editor
    public Sprite frozenSprite; // Assign the frozen state sprite in the Unity Editor

    //Wander
    public bool WithinPlayerRadius;
    public float wanderRadius = 10f; // Adjust this value for the wandering radius
    public float wanderInterval = 5f; // Adjust this value for the interval between destination changes
    public GameObject navMeshGoal;

    void Start()
    {
        AISpriteRenderer.sprite = normalSprite;

        // Ensure the player reference is set
        if (player == null)
        {
            player.GetComponent<MazePlayerController>();
        }

        // Disable automatic rotation of the NavMeshAgent
        navMeshAgent.updateRotation = false;

        isFreezed = false;
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, navMeshAgent.transform.position) > 10)
        {
            WithinPlayerRadius = false;
            SetRandomDestination();
        }
        else
        {
            WithinPlayerRadius = true;
        }

        // Check if the player reference is valid
        if (player != null)
        {
            // Set the destination to the player's position
            navMeshGoal = player;
            navMeshAgent.SetDestination(navMeshGoal.transform.position);
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
        
        // Rotate cone of vision
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            Vector3 movementDirection = navMeshAgent.velocity.normalized;
            movementDirection.y = 0f;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            // Ignore rotation around the y-axis to prevent unwanted tilting
            movementDirection.y = 0f;         

            // Smoothly interpolate between current rotation and target rotation
            ConeOfVision.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, coneRotationSpeed * Time.deltaTime);

            if (isFreezed)
            {
                AISpriteRenderer.sprite = frozenSprite;
                currentPauseTime += Time.deltaTime;

                if (currentPauseTime>freezeTrap.freezeDuration)
                {
                    AISpriteRenderer.sprite = normalSprite;
                    isFreezed = false;
                    currentPauseTime = 0f;
                }
                else
                {
                    AISpriteRenderer.sprite = frozenSprite;
                    // Keep the agent stationary during the pause
                    navMeshAgent.velocity = Vector3.zero;
                    return;
                }
            }
            else
            {
                AISpriteRenderer.sprite = normalSprite;
            }
        }
        else
        {
            if(player.transform.position.magnitude - this.transform.position.magnitude < 10)
            {
                // Set a new destination after the pause duration
                navMeshAgent.SetDestination(navMeshGoal.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0f;
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
