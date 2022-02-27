using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        DontDestroyOnLoad(gameObject);
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
       
        s.source.Play();
        
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (!s.source.isPlaying)
        {
        s.source.Play();
        }
    }

    public void PlayDelayed(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(!s.source.isPlaying)
        {
        s.source.PlayDelayed(0.5f);
        }
    }

     public void StopPlaying (string name)
    {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    s.source.Stop ();
    }
    
}
