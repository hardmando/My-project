using UnityEngine;

public class LampRotation : MonoBehaviour
{
    public float swingSpeed = 2f; // Adjust the speed of the swing
    public float swingAngle = 45f; // Adjust the maximum swing angle in degrees

    private float initialRotation;

    void Start()
    {
        initialRotation = transform.eulerAngles.x;
    }

    void Update()
    {
        // Calculate the swing rotation using a sine wave
        float swingRotation = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(initialRotation + swingRotation, 0f, 0f);
    }
}
