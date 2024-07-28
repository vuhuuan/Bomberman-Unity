using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        foreach (var sound in sounds)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            sound.SetUpAudioSource(newAudioSource);
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.SoundName == name);
        if (s == null) 
        {
            Debug.LogWarning("Sound " + s.SoundName + " not found!");
            return;
        }

        s.SoundAudioSource.Play();
    }
}
