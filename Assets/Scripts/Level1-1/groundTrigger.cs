using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private main main;

    void Start()
    {
        main = GameObject.Find("main").GetComponent<main>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            Debug.Log("This is groundTrigger.cs. The bottle hit the ground and is calling ScoreMinus");
            main.ScoreMinus();
            Destroy(other.gameObject);
        }
    }
}
