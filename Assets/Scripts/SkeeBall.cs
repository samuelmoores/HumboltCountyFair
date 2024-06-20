using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeBall : MonoBehaviour
{
    Rigidbody rb;
    float timer;
    bool startTimer;
    [HideInInspector] public float throwStrength;
    public Vector3 startPosition;
    bool play;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        startTimer = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        throwStrength = 0.0f;
        play = false;
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
            Debug.Log(force);
            rb.AddForce(force);
            throwStrength = 0.0f;
            startTimer = true;
            play = false;
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if(timer > 3.0f)
            {
                startTimer = false;
                timer = 0.0f;
                rb.velocity = Vector3.zero;
                transform.position = startPosition;
                Debug.Log(timer);
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
