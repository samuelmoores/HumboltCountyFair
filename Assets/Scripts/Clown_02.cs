using System;
using UnityEngine.AI;
using UnityEngine;
using System.Security.Cryptography;
using Unity.VisualScripting;

public class Clown_02 : MonoBehaviour
{
    public AudioClip attackSound;
    public AudioClip[] damageSounds;
    public AudioClip[] thwhackSounds;
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
    bool isDead;
    bool damaged;
    float damageTimer;
    float health;
    bool playedDamageSound;
    int damageSoundIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Mechanic");
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        attackTimer = 2.5f;
        isAttacking = false;
        damaged = false;
        damageTimer = 1.15f;
        health = 1.0f;
        ragdollColliders = this.gameObject.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
        playedDamageSound = false;
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
            if (distanceFromPlayer < 10.0f && distanceFromPlayer > 1.0f && !damaged)
            {
                agent.destination = playerPosition;
            }
            else if(distanceFromPlayer < 1.0f && !isAttacking && playerHealth > 0.0f && !damaged)
            {
                isAttacking = true;
            }

            //start attacking
            if(isAttacking && distanceFromPlayer < 1.0f && attackTimer > 0.0f && !damaged)
            {
                Attack();

            }
            else
            {
                isAttacking = false;
                attackTimer = 2.5f;
            }

            if(damaged && damageTimer > 0.0f)
            {
                damageTimer -= Time.deltaTime;
                if(damageTimer < 1.0f && !playedDamageSound)
                {
                    if (damageSoundIndex == 3)
                    {
                        damageSoundIndex = 0;
                    }

                    source.clip = damageSounds[damageSoundIndex++];
                    source.Play();
                    playedDamageSound = true;
                }
            }
            else
            {
                playedDamageSound = false;
                damaged = false;
                damageTimer = 1.15f;
            }
        }
    }

    void PlayWhirlSound()
    {
        source.clip = attackSound;
        source.Play();
    }

    private void Attack()
    {

        attackTimer -= Time.deltaTime;
        
        if (attackTimer < 1.00f && !damaged)
        {
            player.GetComponent<PlayerController>().health -= Time.deltaTime;


        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon") && playerController.attacking && !damaged && attackTimer > 1.6f)
        {
            damaged = true;
            Debug.Log(attackTimer);

            int i = 0;
            if (i < 3)
            {
                i++;
            }
            else
            {
                i = 0;
            }

            source.clip = thwhackSounds[i];
            source.Play();

            attackTimer = 2.0f;
            health -= 0.3f;
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
