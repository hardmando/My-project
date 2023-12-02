using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPoint : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButton("Interact"))
        {
            other.GetComponent<PlayerMovement>().respawnPoint = gameObject.GetComponent<Transform>();
        }
    }
}
