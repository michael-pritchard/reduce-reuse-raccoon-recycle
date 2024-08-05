using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText; // Use Text if not using TextMeshPro
    public Button yesButton;
    public Button exitButton;
    public GameObject binHelpPopup;
    private bool isPlayerInRange = false;
    [SerializeField] public string levelToLoad;

    void Start()
    {
        dialogueBox.SetActive(false);
        binHelpPopup.SetActive(false);
        yesButton.onClick.AddListener(OnYesButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
            HideBinHelp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            ShowBinHelp();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            HideBinHelp() ;
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBox.SetActive(true);
    }

    void HideDialogue()
    {
        dialogueBox.SetActive(false);
    }

    void ShowBinHelp()
    {
        binHelpPopup.SetActive(true);
    }

    void HideBinHelp()
    {

        binHelpPopup.SetActive(false);
    }

    public void OnYesButtonClicked()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnExitButtonClicked()
    {
        HideDialogue();
    }
}
