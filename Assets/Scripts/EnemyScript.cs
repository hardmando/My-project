using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    public float runSpeed = 5f;
    public float inLightRunSpeed = 2.5f;
    public float attackCooldownDuration = 2f; // Cooldown duration for rat's attack
    public int damageAmount = 10; // Damage inflicted by the rat
    public float attackRange = 1f;
    public float health = 15f;

    public Text hpDisplay;

    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;

    private bool playerIsVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        hpDisplay.text = health.ToString();

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

        if (playerIsVisible)
        {
            agent.SetDestination(player.transform.position);
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

        bool IsInPlayerRange()
        {
            // Check if the player is within the rat's attack range
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= attackRange;
        }

        if (IsInFlashlightCone())
        {
            gameObject.GetComponent<NavMeshAgent>().speed = inLightRunSpeed;
        } else
        {
            gameObject.GetComponent<NavMeshAgent>().speed = runSpeed;
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

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsVisible = true;
        }
        else if (other.CompareTag("FlashlightCone"))
        {
            //gameObject.GetComponent<NavMeshAgent>().speed = inLightRunSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlashlightCone"))
        {
            //gameObject.GetComponent<NavMeshAgent>().speed = inLightRunSpeed;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
