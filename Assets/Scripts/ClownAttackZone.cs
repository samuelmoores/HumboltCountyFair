using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAttackZone : MonoBehaviour
{
    Clown clown;
    PlayerController player;
    AudioSource audioSource;
    public AudioClip juggleSong;
    float attackTimer;
    bool songPlaying;
    bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        clown = transform.parent.gameObject.GetComponent<Clown>();
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        attackTimer = juggleSong.length;
        songPlaying = false;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        clown.animator.SetBool("isAttacking", clown.attacking);

        if(songPlaying && attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;

        }
        else
        {
            attackTimer = juggleSong.length;

            if(!canAttack)
            {
                songPlaying = false;
                clown.attacking = false;
                audioSource.Stop();

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !clown.isDead)
        {
            canAttack = true;
            songPlaying = true;
            clown.agent.isStopped = true;
            clown.animator.SetBool("isAttacking", true);
            audioSource.loop = true;
            audioSource.clip = juggleSong;
            audioSource.Play();
            clown.attacking = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && !clown.isDead && songPlaying)
        {
            canAttack = false;

        }
    }


}
