using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] audioSound, sfxSound;
    public AudioSource audioSource, sfxSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Main menu theme");  
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(audioSound, x => x.name == name);
        if(s == null)
        {
            Debug.Log("Audio not found");
        }
        else
        {
            audioSource.clip = s.clip;
            audioSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        if( s == null)
        {
            Debug.Log("Sfx not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
}
