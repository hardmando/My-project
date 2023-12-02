using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightEnabler : MonoBehaviour
{
    private GameObject flashlight;

    private void Update()
    {
        flashlight = GameObject.FindGameObjectWithTag("FlashlightSource");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight.GetComponent<Light>().enabled = true;
        }
    }
}
