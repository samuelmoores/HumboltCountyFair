using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    public GameObject ticket;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().materials[1];
        ticket.GetComponent<MeshRenderer>().enabled = false;
        ticket.GetComponent<SphereCollider>().enabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SkeeBall"))
        {
            GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().materials[2];
            ticket.GetComponent<SphereCollider>().enabled = true;
            ticket.GetComponent<MeshRenderer>().enabled = true;


        }

    }
}
