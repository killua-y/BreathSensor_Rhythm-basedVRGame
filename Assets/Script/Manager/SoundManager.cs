using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public void PlayMusic()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
