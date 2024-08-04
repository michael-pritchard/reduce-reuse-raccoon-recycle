using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private GameObject soundFXObjectPrefab;  // Use GameObject instead of AudioSource

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep the SoundFXManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (soundFXObjectPrefab == null)
        {
            Debug.LogError("SoundFXObjectPrefab is not assigned.");
            return;
        }

        // Spawn the gameObject with AudioSource component
        GameObject audioObject = Instantiate(soundFXObjectPrefab, spawnTransform.position, Quaternion.identity);
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("The spawned object does not have an AudioSource component.");
            Destroy(audioObject);
            return;
        }

        // Assign the audioClip and volume
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Destroy the audio object after the clip has finished playing
        Destroy(audioObject, audioClip.length);
    }
}
