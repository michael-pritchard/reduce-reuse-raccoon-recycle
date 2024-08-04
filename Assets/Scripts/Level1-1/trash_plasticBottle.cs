using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_plasticBottle : MonoBehaviour
{
    main mainScript;
    Transform tr;
    [SerializeField] private AudioClip trash_plasticSoundClip;
    private bool hasCollided = false;

    void Start()
    {
        tr = GetComponent<Transform>();
        mainScript = GameObject.Find("main").GetComponent<main>();
    }

    void FixedUpdate()
    {
        tr.position -= new Vector3(0f, 0.12f, 0f);

        if (tr.position.y < -5f) Destroy(this.gameObject);
    }

    public void HandleCollision()
    {
        if (!hasCollided)
        {
            hasCollided = true;
            if (SoundFXManager.instance != null)
            {
                if (trash_plasticSoundClip != null)
                {
                    SoundFXManager.instance.PlaySoundFXClip(trash_plasticSoundClip, transform, 1f);
                    Debug.Log("Playing sound: " + trash_plasticSoundClip.name);
                }
                else
                {
                    Debug.LogError("AudioClip not assigned.");
                }
            }
            else
            {
                Debug.LogError("SoundFXManager instance is null.");
            }

            mainScript.ScoreAdd();
            Destroy(gameObject);
            Debug.Log("This is from trash_plasticBottle.cs. ScoreAdd is called.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            HandleCollision();
        }
    }
}