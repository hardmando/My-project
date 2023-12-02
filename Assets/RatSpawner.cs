using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject rat;
    public int ratsAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= ratsAmount; i++)
        {
            Instantiate(rat, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        }
    }
}
