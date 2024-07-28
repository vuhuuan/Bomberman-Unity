using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioSource SoundAudioSource;

    public string SoundName;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;
    public bool loop;

    public void SetUpAudioSource(AudioSource audioSource)
    {
        SoundAudioSource = audioSource;

        SoundAudioSource.clip = clip;
        SoundAudioSource.volume = volume;
        SoundAudioSource.pitch = pitch;
        SoundAudioSource.loop = loop;
    }

}
