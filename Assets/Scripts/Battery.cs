using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    float rotationSpeed;
    float angle;
    bool collected;
    float collectionTimer;
    public ParticleSystem ps;
    float translateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.0f;
        collectionTimer = 10.0f;
        translateSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10.0f);
        rotationSpeed += 2.0f;

        collectionTimer -= Time.deltaTime;

        if(collectionTimer > 8.0f)
        {
            translateSpeed += 0.0005f;
            transform.Translate(Vector3.up * Time.deltaTime * translateSpeed);

        }

        if(collectionTimer < 5.0f)
        {
            ps.Play();
            GetComponent<MeshRenderer>().enabled = false;
        }
        
        angle = transform.rotation.y + Time.deltaTime * rotationSpeed;
        transform.Rotate(Vector3.up, angle);
    }

    
}
