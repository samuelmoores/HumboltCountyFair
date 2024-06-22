using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.AI;

public class Clown_02 : MonoBehaviour
{
    public AudioClip attackSound;
    NavMeshAgent agent;
    GameObject player;
    PlayerController playerController;
    Vector3 playerPosition;
    Animator animator;
    AudioSource source;
    Collider[] ragdollColliders;
    Rigidbody[] ragdollRigidbodies;
    float attackTimer;
    bool isAttacking;
    float distanceFromPlayer;
    int velocity;
    float playerHealth;
    bool playSound;
    bool isDead;
    bool damaged;
    float damageTimer;
    float health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Mechanic");
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        attackTimer = 2.0f;
        isAttacking = false;
        playSound = true;
        damaged = false;
        damageTimer = 0.5f;
        health = 1.0f;
        ragdollColliders = this.gameObject.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        //find the distance from player
        if(!isDead)
        {
            playerPosition = player.transform.position;
            playerHealth = player.GetComponent<PlayerController>().health;
            distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
            velocity = (int)Math.Round(agent.velocity.magnitude);
            animator.SetInteger("velocity", velocity);
            animator.SetBool("isAttacking", isAttacking);
            animator.SetBool("isDamaged", damaged);

            //start chasing
            if(distanceFromPlayer < 10.0f && distanceFromPlayer > 1.0f)
            {
                agent.destination = playerPosition;
            }
            else if(distanceFromPlayer < 1.0f && !isAttacking && playerHealth > 0.0f)
            {
                isAttacking = true;
            }

            //start attacking
            if(isAttacking && distanceFromPlayer < 1.0f && attackTimer > 0.0f)
            {
                Attack();

            }
            else
            {
                isAttacking = false;
                playSound = true;
                attackTimer = 2.0f;
            }

            if(damaged && damageTimer > 0.0f)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                damaged = false;
            }
        }
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer < 1.0f && !damaged)
        {
            player.GetComponent<PlayerController>().health -= Time.deltaTime / 2;

            if (playSound)
            {
                source.clip = attackSound;
                source.Play();
                playSound = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon") && playerController.attacking && !damaged && attackTimer > 1.0f)
        {
            Debug.Log("hit by player");
            damaged = true;
            attackTimer = 2.0f;
            health -= 0.4f;
            if(health < 0.0f)
            {
                isDead = true;
                EnableRagdoll();
            }
        }
    }

    void DisableRagdoll()
    {
        foreach (var rb in ragdollRigidbodies)
        {
            if (rb.gameObject != this.gameObject)
                rb.isKinematic = true;
        }

        foreach (Collider collider in ragdollColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                collider.isTrigger = true;
            }

        }

    }

    void EnableRagdoll()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        animator.enabled = false;

        foreach (var rb in ragdollRigidbodies)
        {
            if (rb.gameObject != this.gameObject)
                rb.isKinematic = false;
        }

        foreach (Collider collider in ragdollColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                collider.isTrigger = false;
            }

        }
    }
}
