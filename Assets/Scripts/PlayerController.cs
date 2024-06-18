using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float rotationSpeed;

    float runSpeedDefault;

    float attackCoolDown_default = 0.5f;
    float attackCoolDown_current;

    Animator animator;

    [HideInInspector] public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        runSpeedDefault = runSpeed;
        attackCoolDown_current = attackCoolDown_default;

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(attackCoolDown_current > 0.0f)
        {
            attackCoolDown_current -= Time.deltaTime;
            isAttacking = true;
        }
        else
        {
            runSpeed = runSpeedDefault;
            isAttacking = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && attackCoolDown_current <= 0.0f)
        {
            runSpeed = 0.0f;
            animator.SetTrigger("attack");
            attackCoolDown_current = attackCoolDown_default;
        }

    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            transform.forward = direction;
            transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
