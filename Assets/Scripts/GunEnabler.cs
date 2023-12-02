using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnabler : MonoBehaviour
{
    private GameObject gun;

    private void Update()
    {
        gun = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButton("Interact"))
        {
            gun.GetComponent<PlayerShooting>().enabled = true;
        }
    }
}
