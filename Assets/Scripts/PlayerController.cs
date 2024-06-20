using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Collider[] ragdollColliders;
    Rigidbody[] ragdollRigidbodies;
    Rigidbody rb;


    //----movement--------
    float runSpeed;
    public float runSpeedDefault;
    public float rotationSpeed;

    //-----health-----
    [HideInInspector] public float health;
    [HideInInspector] public bool isDead = false;

    //------attacking------
    float attackCoolDown_default = 1.0f;
    float attackCoolDown_current = 0.0f;
    [HideInInspector] public bool attacking;
    GameObject wrench;

    //------damage-----
    [HideInInspector] public bool damaged = false;
    bool startDamageTimer = false;
    float damageTimer_current;
    float damageTimer_default = 4.25f;
    float damageAnimationTime = 2.25f;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        runSpeed = runSpeedDefault;
        damageTimer_current = damageTimer_default;
        wrench = GameObject.Find("Wrench");
        health = 1.0f;

        rb = GetComponent<Rigidbody>();

        ragdollColliders = this.gameObject.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = this.gameObject.GetComponentsInChildren<Rigidbody>();


        //DisableRagDoll();
        DisableRagdoll_rb();

    }

    // Update is called once per frame
    void Update()
    {
        if (!damaged && !isDead)
        {
            Move();
            Attack();
        }
        TakeDamage();

    }

    private void TakeDamage()
    {
        if(damaged)
            health -= Time.deltaTime / 5;

        if (health < 0.0f && !isDead)
        {
            GetComponent<PlayerSoundEffects>().PlayBodyFallSound();
            isDead = true;
            //EnableRagDoll();
            EnableRagdoll_rb();
        }
        else if (startDamageTimer && damageTimer_current > 0.0f && !attacking)
        {
            damageTimer_current -= Time.deltaTime;

            if (damageTimer_current < damageAnimationTime && !damaged && !isDead)
            {
                animator.SetTrigger("damage");
                ps.Play();
                damaged = true;
            }

        }
        else
        {
            damageTimer_current = damageTimer_default;
            damaged = false;
        }
    }

    void Attack()
    {

        if(attackCoolDown_current > 0.0f)
        {
            attackCoolDown_current -= Time.deltaTime;
            attacking = true;
            wrench.GetComponent<BoxCollider>().enabled = true;
            runSpeed = 0;

        }
        else
        {
            animator.SetBool("isAttacking", false);
            attacking = false;
            wrench.GetComponent<BoxCollider>().enabled = false;
            attackCoolDown_current = 0.0f;
            runSpeed = runSpeedDefault;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
            attackCoolDown_current = attackCoolDown_default;
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));


        if (direction != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            direction.Normalize();
            transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);
        
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ClownAttackZone"))
        {
            startDamageTimer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when to stop damaging the player?
        //exiting damage region
        if(other.CompareTag("ClownAttackZone") && startDamageTimer)
        {
            startDamageTimer = false;
            damageTimer_current = damageTimer_default;
            damaged = false;
        }
    }

    private void DisableRagDoll()
    {
        foreach (Collider collider in ragdollColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                collider.isTrigger = true;
            }

        }

    }

    private void EnableRagDoll()
    {
        animator.enabled = false;
        animator.avatar = null;
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        runSpeed = 0.0f;

        foreach (Collider collider in ragdollColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                collider.isTrigger = false;
                collider.attachedRigidbody.velocity = Vector3.zero;
            }

        }
    }

    void DisableRagdoll_rb()
    {
        foreach (var rb in ragdollRigidbodies)
        {
            if(rb.gameObject != this.gameObject)
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

    void EnableRagdoll_rb()
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
