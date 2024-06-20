using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenMessages : MonoBehaviour
{
    [HideInInspector] public bool showMessage_entrance;

    public GameObject messageBackground;
    public GameObject messageEntrance;

    // Start is called before the first frame update
    void Start()
    {
        showMessage_entrance = false;
        messageEntrance.SetActive(false);
        messageBackground.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("Entrance") && other.CompareTag("Player"))
        {
            Debug.Log("Show Entrance Message");
            messageEntrance.SetActive(true);
            messageBackground.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("Entrance") && other.CompareTag("Player"))
        {
            Debug.Log("Hide Entrance Message");
            messageEntrance.SetActive(false);
            messageBackground.SetActive(false);


        }
    }
}
