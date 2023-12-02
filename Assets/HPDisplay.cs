using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{

    private GameObject player;
    private float hp;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        hp = player.GetComponent<PlayerMovement>().health;
        gameObject.GetComponent<Text>().text = "HP: " + hp.ToString();
    }
}
