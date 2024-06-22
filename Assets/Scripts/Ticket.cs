using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket : MonoBehaviour
{
    float rotationSpeed;
    float angle;
    bool collected;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 200.0f;
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(collected)
        {
            rotationSpeed += 10.0f;

            transform.Translate(Vector3.up * Time.deltaTime);
            Destroy(gameObject, 2.0f);
        }
        
        angle = transform.rotation.y + Time.deltaTime * rotationSpeed;
        transform.Rotate(Vector3.up, angle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            collected = true;
        }
    }
}
