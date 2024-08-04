using UnityEngine;

public class food_apple : MonoBehaviour
{
    main mainScript;
    Transform tr;
    [SerializeField] private AudioClip appleCollectSoundClip;
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
                if (appleCollectSoundClip != null)
                {
                    SoundFXManager.instance.PlaySoundFXClip(appleCollectSoundClip, transform, 1f);
                    Debug.Log("Playing sound: " + appleCollectSoundClip.name);
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

            Destroy(gameObject);
            mainScript.ScoreMinus();
            Debug.Log("this is from food_apple.cs Food apple collected. Score decreased.");
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