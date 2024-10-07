using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static instance of AudioManager to ensure only one exists
    public static AudioManager instance = null;

    // Background music AudioSource
    public AudioSource backgroundMusic;

    // Sound effects AudioSource
    public AudioSource sfxSource;

    // Audio clips for sound effects
    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;

    void Awake()
    {
        // Implement Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // If another instance already exists, destroy this one
        }
        else
        {
            instance = this;  // Set this as the instance
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
    }

    // Play background music
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    // Play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Play specific sounds via helper methods
    public void PlayFlapSound()
    {
        PlaySFX(flapSound);
    }

    public void PlayScoreSound()
    {
        PlaySFX(scoreSound);
    }

    public void PlayDeathSound()
    {
        PlaySFX(deathSound);
    }
}

