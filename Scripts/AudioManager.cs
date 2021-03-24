using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class AudioManager
{
    public enum Sound
    {
        playHitSound,
        playMissSound,
        playWinSound,
    }
    public static void PlayOneShotSound(Sound sound)
    {
        GameObject audioObject = new GameObject("Sound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        
        audioSource.PlayOneShot(GetAudioClip(sound), 1f);

    }

    public static void PlaySound(Sound sound)
    {
        GameObject audioObject = new GameObject("Sound");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = 1f;
        audioSource.PlayDelayed(1);
       

    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameManager.SoundAudioClip soundAudioClip in GameManager.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        }
        return null;
    }
    
}

