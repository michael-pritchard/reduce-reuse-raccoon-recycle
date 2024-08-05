using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonKeyPress : MonoBehaviour
{
    public Button targetButton;
    public typewriterUI typewriterScript;

    void Update()
    {
        if (typewriterScript != null && typewriterScript.IsCompleted())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (targetButton != null)
                {
                    targetButton.onClick.Invoke();
                }
            }
        }
    }
}
