using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    float respawnTimer_deafult = 2.0f;
    float respawnTimer_current;
    public AudioClip[] songs;
    public AudioSource source;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mechanic").GetComponent<PlayerController>();
        respawnTimer_current = respawnTimer_deafult;
        source.volume = 0.1f;

        PlayBGMusic();
    }

    public void PlayBGMusic()
    {
        index = Random.Range(0, 4);
        source.clip = songs[index];
        source.Play();

    }

    public void StopBGMusic()
    {
        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDead && respawnTimer_current > 0.0f)
        {
            respawnTimer_current -= Time.deltaTime;

            if(respawnTimer_current <= 0.0f)
            {
                SceneManager.LoadScene("Level");
            }
        }
    }
}
