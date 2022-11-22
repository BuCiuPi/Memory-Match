using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    
    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume = 0.3f;
    /// the sound fx volume
    [Range(0, 1)]
    public float sfxVolume = 1f;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds and musics: ")]
    public AudioInfoSO[] BackgroundMusic;
    public AudioInfoSO[] SoundEffect;

    Dictionary<string, AudioInfoSO> Audios;

    public override void Awake()
    {
        MakeSingleton(false);
        Init();
    }

    private void Init(){
        Audios = new Dictionary<string, AudioInfoSO>();
        foreach (var item in BackgroundMusic)
        {
            Audios.Add(item.ID,item);
        }

        foreach (var item in SoundEffect)
        {
            Audios.Add(item.ID,item);
        }
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clips">Array of sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip[] clips, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clips != null && clips.Length > 0 && aus)
        {
            var randomIdx = Random.Range(0, clips.Length);
            aus.PlayOneShot(clips[randomIdx], sfxVolume);
        }
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clip">Sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            aus.PlayOneShot(clip, sfxVolume);
        }
    }

    public void PlaySound(string clipID, AudioSource aus = null)
    {
        if(!Audios.ContainsKey(clipID)) return;
        if (!aus)
        {
            aus = sfxAus;
        }

        AudioClip targetClip = Audios[clipID]?.AudioClip;

        if (targetClip!= null && aus)
        {
            aus.PlayOneShot(targetClip, sfxVolume);
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="musics">Array of musics</param>
    /// <param name="loop">Can Loop</param>
    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus && musics != null && musics.Length > 0)
        {
            var randomIdx = Random.Range(0, musics.Length);

            musicAus.clip = musics[randomIdx];
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="music">music</param>
    /// <param name="canLoop">Can Loop</param>
    public void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Set volume for audiosource
    /// </summary>
    /// <param name="vol">New Volume</param>
    public void SetMusicVolume(float vol)
    {
        if (musicAus) musicAus.volume = vol;
    }

    /// <summary>
    /// Stop play music or sound effect
    /// </summary>
    public void StopPlayMusic()
    {
        if (musicAus) musicAus.Stop();
    }

    public void PlayBackgroundMusic()
    {
        List<AudioClip> clips = new List<AudioClip>();
        foreach (var item in BackgroundMusic)
        {
            clips.Add(item.AudioClip);
        }

        PlayMusic(clips.ToArray(), true);
    }
}
