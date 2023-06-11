using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource, bgSoundSource;

    public static float bgmVolume = 1;
    public static float sfxVolume = 1;

    private float deltaTime =10f;
    private float bgSoundDelay = 15f;
    
    
    public void PlayMusic (string name)
    {
        Sound s= Array.Find(musicSound, x => x.name == name);
        

        if (s != null)
        {
            Debug.Log(name);
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
            Debug.Log(name);
            sfxSource.clip = s.clip;
            sfxSource.Play();
            
        }
        else
        {
            Debug.Log("Sound Not Found");
        }
    }
    public void PlayBGSound(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s != null)
        {
            Debug.Log(name);
            bgSoundSource.clip = s.clip;
            bgSoundSource.Play();

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
        musicSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
        bgSoundSource.volume = bgmVolume * 0.8f;

        if(deltaTime <= 0)
        {
            PlayBGSound("BGSFX");
            deltaTime = bgSoundDelay;
        }
        else
        {
            deltaTime -= Time.deltaTime;
        }
    }




}
