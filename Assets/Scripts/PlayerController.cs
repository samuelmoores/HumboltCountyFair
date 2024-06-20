using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    //----movement--------
    float runSpeed;
    public float runSpeedDefault;
    public float rotationSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(!damaged)
        {
            Move();
            Attack();
        }

        if (startDamageTimer && damageTimer_current > 0.0f && !attacking)
        {
            damageTimer_current -= Time.deltaTime;

            if(damageTimer_current < damageAnimationTime && !damaged)
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
}
