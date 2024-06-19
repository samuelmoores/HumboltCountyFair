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
    [HideInInspector] public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        runSpeed = runSpeedDefault;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();

    }

    void Attack()
    {

        if(attackCoolDown_current > 0.0f)
        {
            attackCoolDown_current -= Time.deltaTime;
            isAttacking = true;
            runSpeed = 0;

        }
        else
        {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
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
}
