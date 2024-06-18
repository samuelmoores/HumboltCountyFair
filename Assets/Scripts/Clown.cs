using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    PlayerController playerController;

    bool playerAttacking;
    bool damaged;
    float damageCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        damaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerAttacking = playerController.isAttacking;

        if(damaged && damageCooldown <= 0.0f)
        {
            damaged = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon") && playerAttacking && damageCooldown > 0.0f)
        {
            animator.SetTrigger("damage");
            damaged = true;
            damageCooldown = 0.5f;
        }
    }
}
