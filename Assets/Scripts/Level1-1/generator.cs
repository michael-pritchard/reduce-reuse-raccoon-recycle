using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    float timer = 1;
    public GameObject trashPrefab;
    public GameObject foodPrefab;
    public bool isGenerating = false;

    void Update()
    {
        if (!isGenerating) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            int chance = Random.Range(1, 101);
            float pos_x = Random.Range(-4.0f, 4.0f);

            if (chance <= 20)
            {
                Instantiate(foodPrefab, new Vector3(pos_x, 6.0f, 0.1f), Quaternion.identity);
            }
            else
            {
                Instantiate(trashPrefab, new Vector3(pos_x, 6.0f, 0.1f), Quaternion.identity);
            }

            timer = 0.7f;
        }
    }
}
