using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgmAudioSource; // Reference to the AudioSource component for BGM
    public AudioClip bgmClip; // The audio clip for BGM

    void Start()
    {
        bgmAudioSource.loop = true; // Set the audio source to loop
        bgmAudioSource.clip = bgmClip; // Assign the audio clip to the AudioSource

        if (bgmClip != null && bgmAudioSource != null)
        {
            bgmAudioSource.Play(); // Start playing the BGM
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            PauseMusic();
        }
        else
        {
            ResumeMusic();
        }
    }

    void PauseMusic()
    {
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Pause(); // Pause the music
        }
    }

    void ResumeMusic()
    {
        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Play(); // Resume the music if it was paused
        }
    }
}
