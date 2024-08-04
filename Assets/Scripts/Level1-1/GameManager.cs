using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject readySetGoCanvas;
    public GameObject levelcounter;
    public Image readysetgo_ready;
    public Image readysetgo_set;
    public Image readysetgo_go;
    public AudioClip levelbeginSFX;
    public AudioClip readySetGo_readySFX;
    public AudioClip readySetGo_setSFX;
    public AudioClip readySetGo_goSFX;
    public AudioClip backgroundMusic;
    public float readySetGoDuration = 3f; // Total duration for ready-set-go sequence

    private AudioSource audioSource;

    private SpriteGlowEffect readyGlow;
    private SpriteGlowEffect setGlow;
    private SpriteGlowEffect goGlow;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        readyGlow = readysetgo_ready.GetComponent<SpriteGlowEffect>();
        setGlow = readysetgo_set.GetComponent<SpriteGlowEffect>();
        goGlow = readysetgo_go.GetComponent<SpriteGlowEffect>();
        //is this wha'ts causing my grief
        readySetGoCanvas.SetActive(false);
        StartCoroutine(ShowReadySetGoScreen());
    }

    private IEnumerator ShowReadySetGoScreen()
    {
        /*
        //is this wha'ts causing my griefs
        readySetGoCanvas.SetActive(false);
        */

        // Display the level counter
        float levelBeginDuration = 1f;
        levelcounter.SetActive(true);

        if (levelbeginSFX != null)
        {
            audioSource.PlayOneShot(levelbeginSFX);
        }

        yield return new WaitForSeconds(levelBeginDuration);

        levelcounter.SetActive(false);
        readySetGoCanvas.SetActive(true);


        // Show the ready-set-go screen


        // Calculate individual durations for "Ready," "Set," and "Go"
        float individualDuration = readySetGoDuration / 3;

        // Hide all images initially
        readysetgo_ready.gameObject.SetActive(false);
        readysetgo_set.gameObject.SetActive(false);
        readysetgo_go.gameObject.SetActive(false);

        // Show "Ready"
        readysetgo_ready.gameObject.SetActive(true);
        readyGlow.EnableGlow();
        if (readySetGo_readySFX != null)
        {
            audioSource.PlayOneShot(readySetGo_readySFX);
        }
        yield return new WaitForSeconds(individualDuration);


        // Show "Set"
        readysetgo_set.gameObject.SetActive(true);
        setGlow.EnableGlow();
        if (readySetGo_setSFX != null)
        {
            audioSource.PlayOneShot(readySetGo_setSFX);
        }
        yield return new WaitForSeconds(individualDuration);

        // Show "Go"
        readysetgo_go.gameObject.SetActive(true);
        goGlow.EnableGlow();
        if (readySetGo_goSFX != null)
        {
            audioSource.PlayOneShot(readySetGo_goSFX);
        }
        yield return new WaitForSeconds(individualDuration);
        goGlow.DisableGlow();
        readysetgo_go.gameObject.SetActive(false);


        //disable all three
        readyGlow.DisableGlow();
        setGlow.DisableGlow();
        goGlow.DisableGlow();
        readysetgo_ready.gameObject.SetActive(false);
        readysetgo_set.gameObject.SetActive(false);
        readysetgo_go.gameObject.SetActive(false);

        // Hide the ready-set-go screen
        readySetGoCanvas.SetActive(false);

        // Play background music if it's not already playing
        if (backgroundMusic != null && !audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Enable game controls or start gameplay logic here
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
        }

        generator generatorScript = FindObjectOfType<generator>();
        if (generatorScript != null)
        {
            generatorScript.isGenerating = true;
        }

        main mainScript = FindObjectOfType<main>();
        if (mainScript != null)
        {
            mainScript.GameOver = false;
            mainScript.generatorScript.isGenerating = true;
        }
    }
}
