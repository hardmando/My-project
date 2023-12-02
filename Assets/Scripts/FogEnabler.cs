using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEnabler : MonoBehaviour
{
    [SerializeField] private GameObject fog;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fog.SetActive(true);
        }
    }
}
