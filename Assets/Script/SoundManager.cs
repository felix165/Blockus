using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;

    public static float volume = 100;
    
    
    public void PlayMusic (string name)
    {
        Sound s= Array.Find(musicSound, x => x.name == name);
        

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
           
            musicSource.Play();
            
        }
        else
        {
            Debug.Log("Sound Not Found");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
            
        }
        else
        {
            Debug.Log("Sound Not Found");
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PlayMusic("BGM");
    }

    private void Update()
    {
        musicSource.volume= volume;
        sfxSource.volume= volume;
    }



}
