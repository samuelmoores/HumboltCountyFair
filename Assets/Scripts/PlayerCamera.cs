using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float new_x = player.transform.position.x;
        float curr_y = transform.position.y;

        float new_z = player.transform.position.z - 5.0f;
        
        Vector3 translation = new Vector3(new_x, curr_y, new_z);

        transform.position = translation;
    }
}
