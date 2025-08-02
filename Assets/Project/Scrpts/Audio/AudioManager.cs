using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public string startMusicName;
    public static AudioManager Instance;

    public Sound[] musicSounds;
    public SFXCategory[] sfxCategories;

    public AudioSource musicSource, sfxSource;


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
        if (!string.IsNullOrEmpty(startMusicName))
        {
            PlayMusic(startMusicName);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music not found: " + name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;
            musicSource.Play();
        }
    }

    public void PlaySFX(string category, string name)
    {
        SFXCategory cat = Array.Find(sfxCategories, c => c.categoryName == category);
        if (cat == null)
        {
            Debug.LogWarning($"SFX Category '{category}' not found");
            return;
        }

        Sound s = Array.Find(cat.sounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound '{name}' not found in category '{category}'");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip, s.volume);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
