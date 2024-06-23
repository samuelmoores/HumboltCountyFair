using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnstile : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && player.hasTicket)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
