using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    public void PlayMusic()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(soundObject, clip.length); // Destroy the sound object after the clip finishes
    }
}
