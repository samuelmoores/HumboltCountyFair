using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAttackZone : MonoBehaviour
{
    Clown clown;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        clown = transform.parent.gameObject.GetComponent<Clown>();
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !clown.isDead)
        {
            clown.agent.isStopped = true;
            clown.animator.SetBool("isAttacking", true);
            clown.soundEffects.PlayJuggleSong();
            clown.attacking = true;

        }
    }

    
}
