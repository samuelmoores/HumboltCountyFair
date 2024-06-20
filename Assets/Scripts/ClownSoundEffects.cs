using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ClownSoundEffects : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip juggleSong;
    public AudioClip bloodSplatter;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    void PlayFootStep()
    {
        source.loop = false;
        int i = Random.Range(0, 4);
        source.volume = 1.0f;
        source.clip = footsteps[i];
        source.Play();
    }

    void PlayBloodSplatter()
    {
        source.loop = false;

        source.clip = bloodSplatter;
        source.Play();
    }

    public void PlayJuggleSong()
    {
        source.clip = juggleSong;
        source.loop = true;
        source.Play();
    }

    public void StopJuggleSong()
    {
        source.Stop();
    }
}
