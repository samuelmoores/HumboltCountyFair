using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioClip[] footstep_clips;
    public AudioClip[] attackVoice_clips;
    public AudioClip[] attackWeapon_clips;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void PlaySound(AudioClip[] array, int size, float volume)
    {
        int i = Random.Range(0, size);
        source.volume = volume;
        source.clip = array[i];
        source.Play();
    }
    void FootStep()
    {
        PlaySound(footstep_clips, 4, 1.0f);
    }

    void AttackGrunt()
    {
        PlaySound(attackVoice_clips, 5, 1.0f);

    }

    void AttackSwoosh()
    {
        PlaySound(attackWeapon_clips, 3, 1.0f);

    }
}
