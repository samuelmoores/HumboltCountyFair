using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Clown : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    PlayerController playerController;
    Rigidbody[] ragdollColliders;
    public GameObject[] balls;
    NavMeshAgent agent;
    public ParticleSystem ps;

    bool playerAttacking;
    bool damaged;
    float damageCooldown;
    float damageCooldown_default = 2.0f;
    float health = 1.0f;
    bool isDead = false;

    bool attacking = false;



    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        damaged = false;
        damageCooldown = damageCooldown_default;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
        

        ragdollColliders = this.gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in ragdollColliders)
        {
            rb.isKinematic = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();

        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);


        if (!isDead)
        {
            if(damaged || distanceFromPlayer > 10.0f)
            {
                agent.isStopped = true;
                animator.SetBool("playerFound", false);
            }
            else if (!attacking)
            {
                agent.isStopped = false;
                agent.destination = player.transform.position;
                animator.SetBool("playerFound", true);
           
            }

        }

        

    }

    private void TakeDamage()
    {
        playerAttacking = playerController.attacking;

        if (damaged && damageCooldown > 0.0f)
        {
            damageCooldown -= Time.deltaTime;

        }
        else
        {
            damaged = false;
            damageCooldown = damageCooldown_default;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon") && playerAttacking && !damaged)
        {
            ps.Play();
            damaged = true;
            animator.SetTrigger("damaged");
            
            health -= 0.5f;

            if(health <= 0.0f)
            {
                isDead = true;
                animator.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                //GetComponent<BoxCollider>().enabled = false;

                agent.enabled = false;

                foreach (var rb in ragdollColliders)
                {
                    rb.isKinematic = false;
                }
            }
        }

        if(other.CompareTag("Player") && !isDead)
        {
            agent.isStopped = true;
            animator.SetBool("isAttacking", true);
            attacking = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", false);
            attacking = false;
            
        }
    }
}
