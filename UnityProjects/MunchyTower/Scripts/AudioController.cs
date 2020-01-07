using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSources;

    void Awake()
    {
        audioSources = GetComponentInChildren<AudioSource>();
    }

    public void PlayAudio(int clipNumber)
    {
        audioSources.clip = audioClips[clipNumber];
        audioSources.Play();
    }
}