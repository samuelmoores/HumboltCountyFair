using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRide : MonoBehaviour
{
    PlayerController player;
    public GameObject battery;
    public ParticleSystem ps;
    public ParticleSystem ps_fix;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
        battery.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && player.hasBattery)
        {
            battery.SetActive(true);
            ps.Stop();
            ps_fix.Play();
        }
    }

    
}
