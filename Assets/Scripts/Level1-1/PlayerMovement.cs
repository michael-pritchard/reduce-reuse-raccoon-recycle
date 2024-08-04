using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minX = -4.5f; // minimum x boundary
    public float maxX = 4.5f; // maximum x boundary

    public bool canMove = false;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // starting out this bool is set to false for level starts

        if (!canMove) return;

        // Get horizontal input
        movement.x = Input.GetAxisRaw("Horizontal");

        // Update Animator parameters
        if (movement.x > 0)
        {
            animator.SetInteger("Direction", 1); // Running right
            animator.SetBool("IsRunning", true);
        }
        else if (movement.x < 0)
        {
            animator.SetInteger("Direction", -1); // Running left
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false); // Idle
        }
    }

    void FixedUpdate()
    {
        // Calculate the new position
        float newX = Mathf.Clamp(rb.position.x + movement.x * speed * Time.fixedDeltaTime, minX, maxX);

        // Move the character only on the X axis
        rb.MovePosition(new Vector2(newX, rb.position.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Trash"))
        {
            // Handle collision with trash
            TrashCollision(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Apple"))
        {
            // Handle collision with apple
            AppleCollision(collision.gameObject);
        }
    }

    private void TrashCollision(GameObject trash)
    {
        var trashScript = trash.GetComponent<trash_plasticBottle>();
        if (trashScript != null)
        {
            trashScript.HandleCollision();
        }
        else
        {
            Debug.LogError("trash_plasticBottle component not found on: " + trash.name);
        }
    }

    private void AppleCollision(GameObject apple)
    {
        var appleScript = apple.GetComponent<food_apple>();
        if (appleScript != null)
        {
            appleScript.HandleCollision();
        }
        else
        {
            Debug.LogError("food_apple component not found on: " + apple.name);
        }
    }
}
