using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecyclingBinInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show a UI element asking if the player wants to start Level 1.1
            // If the player accepts, call StartLevel();
        }
    }

    void StartLevel()
    {
        SceneManager.LoadScene("Level1_1");
    }
}
