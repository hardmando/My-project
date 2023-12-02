using UnityEngine;
using System.Collections.Generic;

public class TransparentObjects : MonoBehaviour
{
    public Transform player;
    public LayerMask transparentLayer;
    public float maxDistance = 1078f;

    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, player.position - Camera.main.transform.position, out hit, maxDistance, transparentLayer))
        {
            Debug.Log("Hit");
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            Material material = renderer.material;

            // Store the original color if not already stored
            if (!originalColors.ContainsKey(renderer))
            {
                originalColors.Add(renderer, material.GetColor("_BaseColor"));
            }

            // Adjust transparency by setting the alpha component
            Color originalColor = originalColors[renderer];
            originalColor.a = 0.2f; // Adjust alpha as needed
            material.SetColor("_BaseColor", originalColor);
        }
        else
        {
            // Reset color if not hit
            foreach (var kvp in originalColors)
            {
                Renderer renderer = kvp.Key;
                Material material = renderer.material;
                material.SetColor("_BaseColor", kvp.Value);
            }

            // Clear the list of modified objects
            originalColors.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Camera.main.transform.position, player.position);
    }
}
