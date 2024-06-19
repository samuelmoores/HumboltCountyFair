using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    PlayerController playerController;
    Rigidbody[] ragdollColliders;

    bool playerAttacking;
    bool damaged;
    float damageCooldown;
    float damageCooldown_default = 2.0f;
    float health = 1.0f;
    bool isDead = false;

    bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        damaged = false;
        damageCooldown = damageCooldown_default;

        ragdollColliders = this.gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in ragdollColliders)
        {
            rb.isKinematic = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerAttacking = playerController.isAttacking;

        if (damaged && damageCooldown > 0.0f)
        {
            damageCooldown -= Time.deltaTime;

            if(isDead && damageCooldown < 1.0f)
            {
                animator.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;

                foreach (var rb in ragdollColliders)
                {
                    rb.isKinematic = false;
                }
                damaged = false;
            }

        }
        else
        {
            damaged = false;
            damageCooldown = damageCooldown_default;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && playerAttacking && !damaged)
        {
            damaged = true;
            animator.SetTrigger("damaged");
            health -= 0.5f;

            if(health <= 0.0f)
            {
                isDead = true;
            }
        }
    }
}
