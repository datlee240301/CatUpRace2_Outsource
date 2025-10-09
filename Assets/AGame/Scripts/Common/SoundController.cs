using System;
using System.Collections;
using System.Collections.Generic;
using DucLV;
using MoreMountains.Tools;
using UnityEngine;
using MoreMountains;

[RequireComponent(typeof(MMSoundManager))]
public class SoundController : Singleton<SoundController>
{
    [SerializeField] SoundElement[] soundElements;
    [SerializeField] private float musicVolumeDefault = 1;
    [SerializeField] private float musicVolumeHome = .6f;
    private Dictionary<SoundType, AudioSource> activeSounds = new Dictionary<SoundType, AudioSource>();

    private void Start()
    {
        InitRegisterEvent();
        InitBackgroundMusic();
    }

    private void OnDestroy()
    {
        RemoveRegisterEvent();
    }

    private void InitRegisterEvent()
    {
        EventDispatcher.RegisterListener(EventID.OnChangeMusicVolumeInHome, OnChangeMusicVolumeInHome);
        EventDispatcher.RegisterListener(EventID.OnChangeMusicVolumeInGame, OnChangeMusicVolumeInGame);
    }

    private void RemoveRegisterEvent()
    {
        EventDispatcher.RemoveListener(EventID.OnChangeMusicVolumeInHome, OnChangeMusicVolumeInHome);
        EventDispatcher.RemoveListener(EventID.OnChangeMusicVolumeInGame, OnChangeMusicVolumeInGame);
    }



    public void ChangeSoundVolume(float volume)
    {
        MMSoundManager.Instance.SetVolumeSfx(volume);
        Debug.Log("change sound volume: "+volume);
    }

    public void OnChangeMusicVolumeInGame(object data)
    {
        Debug.Log("music " + SessionPref.GetSettingType(SettingType.Music));
        if (SessionPref.GetStateSetting(SettingType.Music) == false)
        {
            Debug.Log("turn off music: " + musicVolumeHome);
            ChangeBackgroundMusicVolume(musicVolumeHome);
        }
        else
        ChangeBackgroundMusicVolume(musicVolumeDefault);
    }

    private void InitBackgroundMusic()
    {
        Debug.Log("1:"+ SessionPref.GetStateSetting(SettingType.Music));
        if (SessionPref.GetStateSetting(SettingType.Music))
        {
            Debug.Log("1 a");
            PlayBackgroundMusic(SoundType.Background, 1);
            ChangeBackgroundMusicVolume(0);
        }
        else
        {
            Debug.Log("1 b");
            PlayBackgroundMusic(SoundType.Background, 1);
            ChangeBackgroundMusicVolume(0);
        }
    }

    public AudioSource PlaySound(SoundType type, bool loop = false)
    {
        if(SessionPref.GetSettingType(SettingType.Sound) == false) return null;
        AudioClip audioClip = GetAudioClipByType(type);
        if (audioClip != null)
        {
            MMSoundManagerPlayOptions options = MMSoundManagerPlayOptions.Default;
            options.Loop = loop;
            AudioSource audioSource = MMSoundManager.Instance.PlaySound(audioClip, options);

            //MMSoundManager.Instance.StopSound();
            activeSounds[type] = audioSource;
            return audioSource;
        }
        return null;
    }

    public void RestartAllActiveSounds()
    {
        foreach (var sound in activeSounds.Values)
        {
            sound.Stop();
            sound.Play();
        }
    }

    public void StopSound(SoundType type)
    {
        if (activeSounds.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.Stop();
            audioSource.gameObject.SetActive(false);
            activeSounds.Remove(type);
        }
    }

    public void PauseSound(SoundType type)
    {
        if (activeSounds.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.Pause();
        }
    }

    public void ResumeSound(SoundType type)
    {
        if (activeSounds.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.Play();
        }
    }

    public void PlayBackgroundMusic(SoundType type, float volume)
    {
        MMSoundManagerPlayOptions option = MMSoundManagerPlayOptions.Default;
        option.Loop = true;
        option.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Music;
        option.Volume = volume;
        AudioClip audioClip = GetAudioClipByType(type);
        AudioSource audioSource = MMSoundManager.Instance.PlaySound(audioClip, option);
        activeSounds[type] = audioSource;
    }

    public void ChangeBackgroundMusicVolume(float volume)
    {
        Debug.Log("change background music volume: " + SessionPref.GetSettingType(SettingType.Music));
        //MMSoundManager.Instance.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, volume);
        if (SessionPref.GetSettingType(SettingType.Music) == false)
        {
            Debug.Log("turn off music");
            MMSoundManager.Instance.SetVolumeMusic(0);
            return;
        }
        MMSoundManager.Instance.SetVolumeMusic(volume);
    }

    public void ChangeSoundVolume(SoundType type, float volume)
    {
        if (activeSounds.TryGetValue(type, out AudioSource audioSource))
        {
            audioSource.volume = volume;
        }
    }


    public void OnChangeMusicVolumeInHome(object data)
    {
        ChangeBackgroundMusicVolume(musicVolumeHome);
    }

    public AudioClip GetAudioClipByType(SoundType type)
    {
        for (int i = 0; i < soundElements.Length; i++)
        {
            if(soundElements[i].soundType == type)
            {
                return soundElements[i].audioClip;
            }
        }

        return null;
    }
}

[System.Serializable]
public class SoundElement
{
    public AudioClip audioClip;
    public SoundType soundType;

    public SoundElement(AudioClip audioClip, SoundType soundType)
    {
        this.audioClip = audioClip;
        this.soundType = soundType;
    }
}


public enum SoundType
{
    Background,
    BrickBreak,
    EarnCoin,
    Crash,
    EarnHelmet,
    Bomb,
    ButtonClick,
    GameOver,
    BallBreak
}
