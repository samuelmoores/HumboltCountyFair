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
    bool damaged = false;
    bool startDamageTimer = false;
    float damageTimer_current;
    float damageTimer_default = 2.75f;
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

        Debug.Log(damageTimer_current);
        
        if (startDamageTimer && damageTimer_current > 0.0f)
        {
            damageTimer_current -= Time.deltaTime;

            if(damageTimer_current < damageAnimationTime && !damaged && !attacking)
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
            runSpeed = 0;

        }
        else
        {
            animator.SetBool("isAttacking", false);
            wrench.GetComponent<BoxCollider>().enabled = false;
            attacking = false;
            attackCoolDown_current = 0.0f;
            runSpeed = runSpeedDefault;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
            wrench.GetComponent<BoxCollider>().enabled = true;
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
        if(other.CompareTag("Clown") && !startDamageTimer)
        {
            startDamageTimer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Clown") && startDamageTimer && !attacking)
        {
            startDamageTimer = false;
            damageTimer_current = damageTimer_default;
            damaged = false;
        }
    }
}
