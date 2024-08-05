using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Overworld : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters
        if (animator != null)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetBool("isRunning", true);
                if (movement.x > 0)
                {
                    animator.SetInteger("Direction", 1);
                }
                else if (movement.x < 0)
                {
                    animator.SetInteger("Direction", -1);
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        if (rb != null)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}