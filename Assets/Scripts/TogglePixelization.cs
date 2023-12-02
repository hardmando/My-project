using UnityEngine;
using UnityEngine.UI;

public class TogglePixelization : MonoBehaviour
{
    public RenderTexture renderTexture;
    public RawImage rawImage;

    private void Start()
    {
        // Set initial resolution or load it from a saved setting
        SetRenderTextureResolution(455, 256);
    }

    private void Update()
    {
        // Example: Press "R" key to toggle resolution
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Toggle between two resolutions
            if (renderTexture.width == Screen.width)
            {
                SetRenderTextureResolution(455, 256);
            }
            else
            {
                SetRenderTextureResolution(Screen.width, Screen.height);
            }
        }
    }

    private void SetRenderTextureResolution(int width, int height)
    {
        // Release the existing RenderTexture
        if (renderTexture != null)
        {
            renderTexture.Release();
        }

        // Create a new RenderTexture with the specified resolution
        renderTexture = new RenderTexture(width, height, 24)
        {
            name = "CustomRenderTexture", // Optional: Give it a name
            filterMode = FilterMode.Point
            
        };
        Camera.main.targetTexture = renderTexture;

        // Assign the new RenderTexture to the RawImage
        rawImage.texture = renderTexture;
    }
}
