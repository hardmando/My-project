using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.body.CompareTag("Enemy"))
        {
            other.body.GetComponent<EnemyScript>().health -= player.GetComponent<PlayerShooting>().damage;
        }
        Destroy(gameObject);
    }
}
