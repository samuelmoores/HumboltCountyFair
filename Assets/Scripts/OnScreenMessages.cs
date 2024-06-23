using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenMessages : MonoBehaviour
{
    [HideInInspector] public bool showMessage_entrance;

    PlayerController player;
    public GameObject messageBackground;
    public GameObject message;
    string entranceMessage;
    string circusTentClosedMessage;
    string rideBrokenMessage;
    string rideFixedMessage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
        showMessage_entrance = false;
        messageBackground.SetActive(false);
        entranceMessage = "W, A, S, D to move.\n Space to attack and 'E' to interact";
        circusTentClosedMessage = "Ticket required for entrance";
        rideBrokenMessage = "The ride is broken";
        rideFixedMessage = "You fixed the ride!";

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            messageBackground.SetActive(true);
            message.SetActive(true);

            if(gameObject.CompareTag("Entrance"))
            {
                message.GetComponent<TextMeshProUGUI>().text = entranceMessage;
            }

            if (gameObject.CompareTag("CircusTent"))
            {
                
                if(player.hasTicket)
                {
                    messageBackground.SetActive(false);
                    message.SetActive(false);

                }
                else
                {
                    message.GetComponent<TextMeshProUGUI>().text = circusTentClosedMessage;

                }
            }

            if (gameObject.CompareTag("BrokenRide"))
            {
                if(player.hasBattery)
                {
                    message.GetComponent<TextMeshProUGUI>().text = rideFixedMessage;

                }
                else
                {
                    message.GetComponent<TextMeshProUGUI>().text = rideBrokenMessage;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !player.hasBattery)
        {
            messageBackground.SetActive(false);
            message.SetActive(false);

        }

        if (gameObject.CompareTag("BrokenRide"))
        {
            if (player.hasBattery)
            {
                message.GetComponent<TextMeshProUGUI>().fontSize = 12;
                message.GetComponent<TextMeshProUGUI>().text = "Game Developed by Sam Moores\n\n Music by Alexandr Zhelanov\n\n Sounds and Textures from opengameart.org\n";

            }
        }
    }
}
