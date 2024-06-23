using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    PlayerController player;
    AudioSource source;
    public GameObject healSpawn_prefab;

    public Vector3 spawnPosition;
    public AudioClip[] clips;
    bool collected;
    float timer;
    float x;
    float y;
    float z;
    float angle;
    float rotationSpeed;

    void Awake()
    {
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
        source = GetComponent<AudioSource>();
        GetComponent<MeshRenderer>().enabled = true;

        collected = false;
        timer = 0.0f;
        x = 0.0f;
        y = 0.0f;
        z = 0.0f;
        angle = 30.0f;
        rotationSpeed = 3.0f;

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, angle * Time.deltaTime * rotationSpeed, Space.World);

        if (collected)
        {
            timer += Time.deltaTime;

            if(timer > 3.0f)
            {
                GetComponent<MeshRenderer>().enabled = false;
                Destroy(gameObject);
            }
            else
            {
                y += (Time.deltaTime / 2);
                rotationSpeed += Time.deltaTime * 10;
                Vector3 pos = new Vector3(x, y, z);
                transform.position = pos;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !collected)
        {
            player.health = 1.0f;
            collected = true;
            int i = Random.Range(0, 3);
            source.clip = clips[i];
            source.Play();
            
        }
    }
}
