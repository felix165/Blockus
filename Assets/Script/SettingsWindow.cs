using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance;
    public Slider bgm;
    public Slider sfx;
    // Start is called before the first frame update
    void Start()
    {
        bgm.value=SoundManager.bgmVolume;

        sfx.value = SoundManager.sfxVolume;
        
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.bgmVolume = bgm.value;
        SoundManager.sfxVolume= sfx.value;      
        
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
            this.gameObject.SetActive(false);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void setActive(bool state)
    {
        this.gameObject.SetActive(state);
    }
    public bool getActiveSelf()
    {
        return this.gameObject.activeSelf;
    }

}
