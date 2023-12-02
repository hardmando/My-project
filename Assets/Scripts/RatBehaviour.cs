using UnityEngine;
using UnityEngine.AI;

public class RatBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    public float runAwayDistance = 5f;
    public float runAwaySpeed = 10f;
    public float runSpeed = 8f;
    public float cooldownDuration = 3f; // Cooldown duration in seconds
    public float attackCooldownDuration = 1f; // Cooldown duration for rat's attack
    public int damageAmount = 10; // Damage inflicted by the rat
    public float attackRange = 1f;

    private bool isCoolingDown = false;
    private float cooldownTimer = 0f;

    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;

    private bool playerIsVisible = false;
    private bool _playerIsInLight;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        _playerIsInLight = player.GetComponent<PlayerMovement>().isInLight();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && IsInPlayerRange())
        {
            // Perform rat's attack
            AttackPlayer();
        }

        // Update attack cooldown timer
        if (isAttacking)
        {
            attackCooldownTimer -= Time.deltaTime;

            if (attackCooldownTimer <= 0f)
            {
                isAttacking = false;
            }
        }

        bool IsInPlayerRange()
        {
            // Check if the player is within the rat's attack range
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= attackRange;
        }

        void AttackPlayer()
        {
            // Perform the attack
            // For example, reduce player health
            // PlayerHealth.TakeDamage(damageAmount);
            player.GetComponent<PlayerMovement>().health -= damageAmount;

            // Start the attack cooldown
            isAttacking = true;
            attackCooldownTimer = attackCooldownDuration;
        }

         if (IsInFlashlightCone())
        {
            if (!isCoolingDown)
            {
                // Calculate the direction away from the player
                Vector3 runAwayDirection = transform.position - player.transform.position;
                runAwayDirection.Normalize();

                // Set the destination for the rat to run away
                Vector3 runAwayDestination = transform.position + runAwayDirection * runAwayDistance;
                agent.SetDestination(runAwayDestination);
            }
        }
        else
        {
            if (playerIsVisible)
            {
                // If not in the cone, move towards the player
               
                agent.SetDestination(player.transform.position);
                
            }
        }

        // Update cooldown timer
        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
            }
        }
    }

    Vector3 FindEscapePoint(Vector3 currentPosition, Vector3 runAwayDirection)
    {
        NavMeshPath path = new NavMeshPath();

        // Calculate the escape path
        NavMesh.CalculatePath(currentPosition, currentPosition + runAwayDirection * runAwayDistance, NavMesh.AllAreas, path);

        // Check if the path is valid
        if (path.status == NavMeshPathStatus.PathComplete && path.corners.Length > 1)
        {
            // Return the first point in the path
            return path.corners[1];
        }

        // If the path is not valid, return the original position
        return currentPosition;
    }

    bool IsInFlashlightCone()
    {
        // Check if the rat is within the FlashlightCone
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f); // Adjust the radius as needed

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("FlashlightCone"))
            {
                return true;
            }
        }

        return false;
    }


    // Ensure that the GameObject has a Collider with "Is Trigger" checked
    // Also, if the other Collider doesn't have a Rigidbody, make sure this GameObject has one
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlashlightCone"))
        {
            gameObject.GetComponent<NavMeshAgent>().speed = runAwaySpeed;
            gameObject.GetComponent<NavMeshAgent>().angularSpeed = 10000f;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlashlightCone"))
        {
            gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;
            gameObject.GetComponent<NavMeshAgent>().angularSpeed = 120f;
        } else if (other.CompareTag("Player"))
        {
            playerIsVisible = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsVisible = true;
        } else if (other.CompareTag("FlashlightCone"))
        {
            gameObject.GetComponent<NavMeshAgent>().speed = runAwaySpeed;
            gameObject.GetComponent<NavMeshAgent>().angularSpeed = 10000f;
        }
    }
}
