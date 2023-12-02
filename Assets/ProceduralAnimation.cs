using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ProceduralAnimation : MonoBehaviour
{
    public Transform leftLegRig;
    public Transform rightLegRig;
    public float stepDistance = 0.5f;
    public float stepDuration = 0.2f;
    public float footMoveThreshold = 0.1f;

    private Vector3 originalLocalPosition;
    private PlayerMovement playerMovement;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        playerMovement = GetComponent<PlayerMovement>();
        StartCoroutine(ProceduralRunAnimationCoroutine());
    }

    IEnumerator ProceduralRunAnimationCoroutine()
    {
        while (true)
        {
            // Calculate the displacement of the player's local position from the original local position
            Vector3 currentLocalPosition = transform.localPosition;
            float displacement = Vector3.Distance(originalLocalPosition, currentLocalPosition);

            // Check if the player's position has moved a certain distance
            if (displacement > footMoveThreshold)
            {
                // Move both feet forward
                MoveFoot(leftLegRig, stepDistance);
                MoveFoot(rightLegRig, stepDistance);
                yield return new WaitForSeconds(stepDuration);

                // Update the original local position after each step
                originalLocalPosition = currentLocalPosition;
            }
            else
            {
                // Player hasn't moved enough, wait for a short time
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void MoveFoot(Transform legRig, float distance)
    {
        Vector3 currentLocalPosition = legRig.position;

        // Calculate the target local position based on the movement vector
        Vector3 movementDirection = (transform.localPosition - currentLocalPosition).normalized;
        Vector3 targetLocalPosition = currentLocalPosition + movementDirection * distance;

        StartCoroutine(LerpFootPosition(legRig, currentLocalPosition, targetLocalPosition, stepDuration));
    }

    IEnumerator LerpFootPosition(Transform legRig, Vector3 start, Vector3 end, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);

            Vector3 lerpedPosition = Vector3.Lerp(start, end, t);
            legRig.position = lerpedPosition;

            // Check if the foot is close to the target local position
            float distanceToTarget = Vector3.Distance(lerpedPosition, end);
            if (distanceToTarget < footMoveThreshold)
            {
                // If close, break out of the loop
                break;
            }

            yield return null;
        }

        // Ensure the foot position doesn't go higher than the ground level
        legRig.position = new Vector3(legRig.position.x, Mathf.Min(legRig.position.y, end.y), legRig.position.z);
    }
}
