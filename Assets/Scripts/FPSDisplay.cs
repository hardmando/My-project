using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public float avgFrameRate;
    public Text display_Text;

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}