using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager_Title : MonoBehaviour
{
    public static BackgroundMusicManager_Title instance; // Singleton instance
    public AudioClip musicClip; // Reference to the music clip

    private AudioSource audioSource;

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // Set this as the instance
            instance = this;
            // Don't destroy this object when loading new scenes
            DontDestroyOnLoad(gameObject);
            // Get or add the AudioSource component
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            // Play the music
            if (musicClip != null)
            {
                audioSource.clip = musicClip;
                audioSource.loop = true; // Loop the music
                audioSource.Play();
            }
        }
        else
        {
            // Destroy duplicate instance
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Check if the current scene is Scene 2, if so, destroy this object
        if (SceneManager.GetActiveScene().name == "overworld")
        {
            Destroy(gameObject);
        }
    }
}