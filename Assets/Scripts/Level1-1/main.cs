using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public bool GameOver;
    public Transform title;
    public Text scoreboard;
    public Text timerText;
    public GameObject levelCompleteGroup;
    public GameObject levelFailedGroup;
    public GameObject generatorObject;
    public GameObject BackgroundMusic;
    public GameObject GameManager;
    public int score = 0;

    public generator generatorScript;
    private AudioSource backgroundMusicObject;
    private int streakCount = 0;
    private int multiplier = 1;
    private float levelTime = 30f; // Level duration in seconds
    private int scoreThreshold = 20; // Score threshold for level completion

    [SerializeField] private AudioClip audioFX_successFanfare;

    // Multiplier Icons
    public GameObject MultiplierIcons; // Canvas group
    public GameObject x2MultiplierIcon;
    public GameObject x3MultiplierIcon;
    public GameObject breakingX2Icon;
    public GameObject breakingX3Icon;

    void Start()
    {
        score = 0;
        GameOver = true; // Initially set to true until ready-set-go sequence completes
        levelCompleteGroup.SetActive(false);
        levelFailedGroup.SetActive(false);
        GameManager.SetActive(true);
        UpdateScoreText();
        UpdateTimerText();

        generatorScript = generatorObject.GetComponent<generator>();
        generatorScript.isGenerating = false; // Initially not generating

        // Initialize multiplier icons
       MultiplierIcons.SetActive(false);
        x2MultiplierIcon.SetActive(false);
        x3MultiplierIcon.SetActive(false);
        breakingX2Icon.SetActive(false);
        breakingX3Icon.SetActive(false);

    }

    void Update()
    {
        if (!GameOver)
        {
            levelTime -= Time.deltaTime;
            UpdateTimerText();

            if (levelTime <= 0)
            {
                levelTime = 0;
                EndLevel();
            }

            UpdateMultiplierIcons();

        }

        if (GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        void UpdateMultiplierIcons()
        {
            // Ensure the MultiplierIcons group is active
            MultiplierIcons.SetActive(true);

            // Update icons based on multiplier value
            switch (multiplier)
            {
                case 3:
                    x2MultiplierIcon.SetActive(false);
                    x3MultiplierIcon.SetActive(true);
                    breakingX2Icon.SetActive(false);
                    breakingX3Icon.SetActive(false);
                    break;

                case 2:
                    x2MultiplierIcon.SetActive(true);
                    x3MultiplierIcon.SetActive(false);
                    breakingX2Icon.SetActive(false);
                    breakingX3Icon.SetActive(false);
                    break;

                default:
                    x2MultiplierIcon.SetActive(false);
                    x3MultiplierIcon.SetActive(false);
                    breakingX2Icon.SetActive(false);
                    breakingX3Icon.SetActive(false);
                    break;
            }
        }

    }

    public void ScoreAdd()
    {
        if (levelTime != 0)
        {
            streakCount++;
            Debug.Log($"Streak Count: {streakCount}");

            if (streakCount >= 20)
            {
                multiplier = 3;
            }
            else if (streakCount >= 10)
            {
                multiplier = 2;
            }
            else
            {
                multiplier = 1;
            }
         

            StartCoroutine(HideBreakingIcons());

            score += 1 * multiplier; // add pts to score based on the streak multiplier
            Debug.Log("this is from main. Player earned: " + (1 * multiplier) + " points!");
            UpdateScoreText();
        }
    }
    IEnumerator HideBreakingIcons()
    {
        yield return new WaitForSeconds(0.5f); // Adjust this delay as needed
        breakingX2Icon.gameObject.SetActive(false);
        breakingX3Icon.gameObject.SetActive(false);
    }

    public void ScoreMinus()
    {
        if (levelTime != 0)
        {
            streakCount = 0;
            multiplier = 1; // the player broke the streak by losing out
            score -= 1;
            Debug.Log("this is ScoreMinus from main.cs. Player lost 1 point :(");
            if (score < 0) score = 0;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreboard.text = score.ToString();
    }

    void UpdateTimerText()
    {
        timerText.text = Mathf.Ceil(levelTime).ToString();
    }

void EndLevel()
    {
        GameOver = true;
        generatorScript.isGenerating = false;

        // Stop the background music
        if (BackgroundMusic != null)
        {
            Destroy(BackgroundMusic);
        }

        if (score >= scoreThreshold)
        {
            levelCompleteGroup.SetActive(true);
            if (SoundFXManager.instance != null)
            {
                SoundFXManager.instance.PlaySoundFXClip(audioFX_successFanfare, transform, 1f);
                Debug.Log("Playing sound: " + audioFX_successFanfare.name);
            }
            else
            {
                Debug.LogError("SoundFXManager instance is null.");
            }

            Debug.Log("We beat the level!");
        }
        else
        {
            levelFailedGroup.SetActive(true);
            Debug.Log("We failed the level!");
        }
    }


    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting.");
    }
}
