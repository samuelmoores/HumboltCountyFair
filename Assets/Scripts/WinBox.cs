using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    public GameObject ticket;
    public AudioClip winSound;
    public ParticleSystem ps;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().materials[1];
        ticket.GetComponent<MeshRenderer>().enabled = false;
        ticket.GetComponent<SphereCollider>().enabled = false;
        player = GameObject.Find("Mechanic");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SkeeBall"))
        {
            AudioSource.PlayClipAtPoint(winSound, player.transform.position, 3.0f);
            ps.Play();
            GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().materials[2];
            ticket.GetComponent<SphereCollider>().enabled = true;
            ticket.GetComponent<MeshRenderer>().enabled = true;


        }

    }
}
