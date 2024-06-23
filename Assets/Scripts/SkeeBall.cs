using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeBall : MonoBehaviour
{
    Rigidbody rb;
    public GameObject clown_02_prefab;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    GameObject clown_02;
    public ParticleSystem ps;

    float timer;
    bool startTimer;
    [HideInInspector] public float throwStrength;
    public Vector3 startPosition;
    bool play;
    bool clownSpawned;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        startTimer = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        throwStrength = 0.0f;
        play = false;
        clownSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && throwStrength < 1.0f && play)
        {
            throwStrength += Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            Vector3 force = new Vector3(0.0f, 0.0f, 1000.0f * throwStrength);
            rb.AddForce(force);
            throwStrength = 0.0f;
            startTimer = true;
            play = false;
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if(timer > 2.0f)
            {
                startTimer = false;
                timer = 0.0f;
                rb.velocity = Vector3.zero;
                transform.position = startPosition;
                Debug.Log(transform.position);
                play = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            play = true;
            rb.useGravity = true;

            if(!clownSpawned)
            {
                clownSpawned = true;
                clown_02 = GameObject.Instantiate(clown_02_prefab, spawnPosition, spawnRotation);
                ps.Play();

            }
            

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            play = false;
        }
    }
}
