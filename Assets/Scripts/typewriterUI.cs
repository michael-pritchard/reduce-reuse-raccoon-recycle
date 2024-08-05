using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class typewriterUI : MonoBehaviour
{
    Text _text;
    TMP_Text _tmpProText;
    string writer;
    bool isCompleted = false; // To check if the text has been completed or not

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    void Start()
    {
        _text = GetComponent<Text>();
        _tmpProText = GetComponent<TMP_Text>();

        if (_text != null)
        {
            writer = _text.text;
            _text.text = "";

            StartCoroutine(TypeWriterText());
        }

        if (_tmpProText != null)
        {
            writer = _tmpProText.text;
            _tmpProText.text = "";

            StartCoroutine(TypeWriterTMP());
        }
    }

    void Update()
    {
        // Check for player input to complete text
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) // Change to Start if using a gamepad
        {
            if (!isCompleted)
            {
                StopAllCoroutines(); // Stop the typewriter effect
                CompleteText(); // Complete the text immediately
                isCompleted = true;
            }
        }
    }

    IEnumerator TypeWriterText()
    {
        _text.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (isCompleted) yield break; // Exit coroutine if text is completed

            if (_text.text.Length > 0)
            {
                _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
            }
            _text.text += c;
            _text.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
        }

        isCompleted = true; // Mark the text as completed
    }

    IEnumerator TypeWriterTMP()
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (isCompleted) yield break; // Exit coroutine if text is completed

            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }

        isCompleted = true; // Mark the text as completed
    }

    void CompleteText()
    {
        if (_text != null)
        {
            _text.text = writer;
        }

        if (_tmpProText != null)
        {
            _tmpProText.text = writer;
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}
