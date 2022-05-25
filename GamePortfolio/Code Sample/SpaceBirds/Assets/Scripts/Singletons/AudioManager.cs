using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundAudioClip
{
    public AudioManager.Sound sound;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public SoundAudioClip[] soundAudioClips;

    public AudioSource fxAudioSource;
    public AudioSource musicSource;

    public enum Sound
    {
        PlayerShoot,
        PlayerDie,
        EnemyShoot,
        EnemyDie,
        PowerUp,
        EnemyHit
    }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }
    
    public AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAudioClip soundAudioClip in soundAudioClips)
        {
            if (soundAudioClip.sound == sound) return soundAudioClip.audioClip;
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    public void PlaySound(Sound sound)
    {
        fxAudioSource.PlayOneShot(GetAudioClip(sound));
    }
}
