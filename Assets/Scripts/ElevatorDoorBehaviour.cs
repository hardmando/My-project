using UnityEngine;
using UnityEngine.UI;

public class ElevatorDoorBehaviour : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 3f;
    public float doorSpeed = 2f;

    private bool doorsOpen = false;
    private Vector3 initialPosition;

    [Header("Door Side")]
    public bool isRightDoor = true;

    void Start()
    {
        // Store the initial position of the doors
        initialPosition = transform.position;
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Check the distance between the player and the doors
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the activation distance and the doors are not already open, start opening them
        if (distanceToPlayer < activationDistance && !doorsOpen)
        {
            OpenDoors();
        }
        // If the player is outside the activation distance and the doors are open, start closing them
        else if (distanceToPlayer >= activationDistance && doorsOpen)
        {
            CloseDoors();
        }
    }

    void OpenDoors()
    {
        if (!isRightDoor)
        {
            // Lerp the doors towards the open position
            transform.position = Vector3.Lerp(transform.position, initialPosition + Vector3.right * 5f, Time.deltaTime * doorSpeed);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition + Vector3.left * 5f, Time.deltaTime * doorSpeed);
        }
        // Optionally, you can play an opening sound or trigger other effects here

        // Update the state
        doorsOpen = true;
    }

    void CloseDoors()
    {
        // Lerp the doors towards the initial closed position
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * doorSpeed);

        // Optionally, you can play a closing sound or trigger other effects here

        // Update the state
        doorsOpen = false;
    }
}
