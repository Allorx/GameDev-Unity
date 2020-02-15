using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource[] audioSources;

    void Awake()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void PlayAudio(int clipNumber)
    {
        audioSources[0].clip = audioClips[clipNumber];
        audioSources[0].Play();
    }

    public void StopMusic()
    {
        audioSources[1].Stop();
    }
}